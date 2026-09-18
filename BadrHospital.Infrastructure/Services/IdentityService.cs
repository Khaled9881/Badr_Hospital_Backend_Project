using BadrHospital.Application.Interfaces;
using HospitalManagementSystem.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;


namespace BadrHospital.Infrastructure.Services
{
    public class IdentityService(UserManager<ApplicationUser> userManager) : IIdentityService
    {
        public async Task<bool> FindByEmailAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            return user != null;
        }

        public async Task<bool> FindByNameAsync(string name)
        {
            var user = await userManager.FindByNameAsync(name);
            return user != null;
        }

        public async Task<(IdentityResult Result, Guid UserId)> CreateUserAsync(string Email, string Password, string userName, string? PhoneNumber)
        {
            var user = new ApplicationUser()
            {
                Email = Email,
                UserName = userName,
                PhoneNumber = PhoneNumber,
            };

            var result = await userManager.CreateAsync(user, Password);
            return (result, user.Id);
        }

        public async Task<IdentityResult> AddtoRoleAsync(string userId, string role)
        {
            var user = await userManager.FindByIdAsync(userId);
            return await userManager.AddToRoleAsync(user, role);
        }

    }
}
