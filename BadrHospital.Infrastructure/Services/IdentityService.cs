using BadrHospital.Application.Common;
using BadrHospital.Application.Interfaces;
using HospitalManagementSystem.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;


namespace BadrHospital.Infrastructure.Services
{
    public class IdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : IIdentityService
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

        public async Task<(bool, Guid, List<string>?)> SigninAsync(string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user is null)
                return (false, Guid.Empty, new List<string>());

            var result = await signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!result.Succeeded)
                return (false, Guid.Empty, new List<string>());

            var roles = await userManager.GetRolesAsync(user);


            return (true, user.Id, roles.ToList());
        }

        public async Task<string> ForgetPassword(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user is null)
                return string.Empty;

            return await userManager.GeneratePasswordResetTokenAsync(user);

        }

        public async Task<IdentityResult> ResetPassword(Guid id, string resetToken, string newPassword)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user is null)
                return IdentityResult.Failed(new IdentityError { Description = "Invalid request." });

            return await userManager.ResetPasswordAsync(user, resetToken, newPassword);
        }

        public async Task<Guid> GetUserIdByEmailAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user is null)
                return Guid.Empty;
            return user.Id;
        }
    }
}
