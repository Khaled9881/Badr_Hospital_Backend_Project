using Microsoft.AspNetCore.Identity;


namespace BadrHospital.Application.Interfaces
{
    public interface IIdentityService
    {
        public Task<bool> FindByEmailAsync(string email);
        public Task<bool> FindByNameAsync(string name);
        public Task<(IdentityResult Result, Guid UserId)> CreateUserAsync(string Email, string Password, string userName, string? PhoneNumber);
        public Task<IdentityResult> AddtoRoleAsync(string userId, string role);

    }
}
