using AutoMapper;
using CRUD_Operations.Dtos;
using CRUD_Operations.Helpers;
using CRUD_Operations.Models;
using CRUD_Operations.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CRUD_Operations.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _jwt = jwt.Value;
        }

        public async Task<Response<AuthModel>> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName); 
            if (user is null)
            {
                return new Response<AuthModel>
                {
                    Message = "Invalid credentials",
                    StatusCode = HttpStatusCode.Unauthorized
                };
            }
            // Prevent login before email confirmation
            if (!user.EmailConfirmed)
            {
                return new Response<AuthModel>
                {
                    Message = "Email not confirmed. Please confirm your email before logging in.",
                    StatusCode = HttpStatusCode.Forbidden
                };
            }
            var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValid)
            {
                return new Response<AuthModel>
                {
                    Message = "Invalid credentials",
                    StatusCode = HttpStatusCode.Unauthorized
                };
            }

            var jwtSecurityToken = await CreateJwtToken(user);


            return new Response<AuthModel>
            {
                StatusCode = HttpStatusCode.OK,
                Data = new AuthModel
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault(),
                    UserName = user.UserName
                },
                
                Message = "Login successful"
            };
        }

        public async Task<Response<AuthModel>> Register(RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
            {
                return new Response<AuthModel>
                {
                    Message = "Email already exists",
                    StatusCode = HttpStatusCode.Conflict
                };
            }
            var user = _mapper.Map<ApplicationUser>(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            await _userManager.AddToRoleAsync(user , model.Role);


            if (!result.Succeeded)
            {
                var errors = string.Empty;
                errors += result.Errors.Select(x=>x.Description).ToString();
                return new Response<AuthModel>
                {
                    Message = errors,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            var jwtSecurityToken = await CreateJwtToken(user);

            return new Response<AuthModel>
            {
                Message = "User created successfully",
                StatusCode = HttpStatusCode.OK,
                Data = new AuthModel
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    Role = model.Role,
                    UserName = user.UserName
                }
            };

        }

        public async Task<Response<string>> ResetPassword(string email, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new Response<string>
                {
                    Message = "User not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            // Remove the current password hash and set the new one directly
            // This approach bypasses the need for token providers since we've already validated via OTP
            await _userManager.RemovePasswordAsync(user);
            var result = await _userManager.AddPasswordAsync(user, newPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(x => x.Description));
                return new Response<string>
                {
                    Message = errors,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            // Since the user has verified their email through OTP, mark email as confirmed
            if (!user.EmailConfirmed)
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
            }

            return new Response<string>
            {
                Message = "Password reset successfully",
                StatusCode = HttpStatusCode.OK,
                Data = "Password updated"
            };
        }

        public async Task<Response<string>> ChangePassword(string userId, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new Response<string>
                {
                    Message = "User not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            // Verify current password and change to new password
            // This method automatically invalidates the old password hash
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(x => x.Description));
                return new Response<string>
                {
                    Message = errors,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            return new Response<string>
            {
                Message = "Password changed successfully. Old password is no longer valid.",
                StatusCode = HttpStatusCode.OK,
                Data = "Password updated"
            };
        }


        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name , user.UserName),
                new Claim(ClaimTypes.NameIdentifier , user.Id),
                new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role , roles.FirstOrDefault())
            }
            .Union(userClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey , SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.ExpireDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
