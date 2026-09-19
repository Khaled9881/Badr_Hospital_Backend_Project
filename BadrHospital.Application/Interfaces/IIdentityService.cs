using Microsoft.AspNetCore.Identity;


namespace BadrHospital.Application.Interfaces
{
    public interface IIdentityService
    {
        public Task<bool> FindByEmailAsync(string email);
        public Task<bool> FindByNameAsync(string name);
        public Task<Guid> GetUserIdByEmailAsync(string email);
        public Task<(IdentityResult Result, Guid UserId)> CreateUserAsync(string Email, string Password, string userName, string? PhoneNumber);
        public Task<IdentityResult> AddtoRoleAsync(string userId, string role);
        public Task<(bool, Guid, List<string>?)> SigninAsync(string email, string password);

        public Task<string> ForgetPassword(Guid id);
        public Task<IdentityResult> ResetPassword(Guid id, string resetToken, string newPassword);
    }
}
