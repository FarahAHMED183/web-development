using CRUD_Operations.Dtos;
using CRUD_Operations.Models;

namespace CRUD_Operations.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<Response<AuthModel>> Register(RegisterModel model);
        public Task<Response<AuthModel>> Login(LoginModel model);
        public Task<Response<string>> ChangePassword(string userId, string currentPassword, string newPassword);
        public Task<Response<string>> ResetPassword(string email, string newPassword);
    }
}
