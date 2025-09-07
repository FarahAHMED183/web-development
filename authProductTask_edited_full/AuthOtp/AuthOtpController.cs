using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using CRUD_Operations.Services.Interfaces;
using CRUD_Operations.Dtos;

namespace AuthOtp
{
    public class EmailOptions
    {
        public int OtpTtlSeconds { get; set; } = 300;
        public int SessionTtlSeconds { get; set; } = 600;
    }

    [ApiController]
    [Route("api/[controller]")]
    public class AuthOtpController : ControllerBase
    {
        private readonly IEmailSender _emailSender;
        private readonly IOtpService _otpService;
        private readonly EmailOptions _emailOpts;
        private readonly IAuthService _authService;

        public AuthOtpController(IEmailSender emailSender, IOtpService otpService, IOptions<EmailOptions> emailOpts, IAuthService authService)
        {
            _emailSender = emailSender;
            _otpService = otpService;
            _emailOpts = emailOpts.Value;
            _authService = authService;
        }

        [HttpPost("send-confirmation")]
        public async Task<IActionResult> SendConfirmation([FromBody] SendEmailRequest req)
        {
            if (string.IsNullOrEmpty(req.Email)) return BadRequest("email required");
            var otp = await _otpService.GenerateAndStoreOtpAsync(req.Email, TimeSpan.FromSeconds(_emailOpts.OtpTtlSeconds));
            var body = $"<p>Your confirmation code is <strong>{otp}</strong></p>";
            await _emailSender.SendEmailAsync(req.Email, "Email confirmation code", body);
            return Ok(new { sent = true });
        }

        [HttpPost("verify-confirmation")]
        public async Task<IActionResult> VerifyConfirmation([FromBody] VerifyOtpRequest req)
        {
            if (string.IsNullOrEmpty(req.Email) || string.IsNullOrEmpty(req.Otp)) return BadRequest("email and otp required");
            var ok = await _otpService.ValidateOtpAsync(req.Email, req.Otp);
            if (!ok) return BadRequest("invalid or expired otp");
            // TODO: set user's email as confirmed in your user store here.
            return Ok(new { confirmed = true });
        }

        [HttpPost("send-forget")]
        public async Task<IActionResult> SendForget([FromBody] SendEmailRequest req)
        {
            if (string.IsNullOrEmpty(req.Email)) return BadRequest("email required");
            var otp = await _otpService.GenerateAndStoreOtpAsync(req.Email, TimeSpan.FromSeconds(_emailOpts.OtpTtlSeconds));
            var body = $"<p>Your password reset code is <strong>{otp}</strong></p>";
            await _emailSender.SendEmailAsync(req.Email, "Password reset code", body);
            return Ok(new { sent = true });
        }

        [HttpPost("verify-change")]
        public async Task<IActionResult> VerifyChange([FromBody] VerifyOtpRequest req)
        {
            if (string.IsNullOrEmpty(req.Email) || string.IsNullOrEmpty(req.Otp)) return BadRequest("email and otp required");
            var ok = await _otpService.ValidateOtpAsync(req.Email, req.Otp);
            if (!ok) return BadRequest("invalid or expired otp");
            var session = _otpService.CreateSessionToken(req.Email, TimeSpan.FromSeconds(_emailOpts.SessionTtlSeconds));
            return Ok(new { session });
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest req)
        {
            if (string.IsNullOrEmpty(req.Session) || string.IsNullOrEmpty(req.NewPassword)) return BadRequest("session and newPassword required");
            var (ok, userId) = _otpService.ValidateSessionToken(req.Session);
            if (!ok) return BadRequest("invalid or expired session");
            
            // Actually change the password using AuthService
            var result = await _authService.ResetPassword(userId, req.NewPassword);
            
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(new { changed = true, message = result.Message });
            }
            
            return BadRequest(new { changed = false, message = result.Message });
        }
    }
}
