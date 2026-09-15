using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BadrHospital.Infrastructure.Identity
{
    public static class RoleSeedData
    {
        public static readonly string[] Roles =
        {
            "Admin",
            "Receptionist",
            "Doctor",
            "Patient",
            "Pharmacist",
            "LabTechnician"
        };

        public static async Task SeedAsync(RoleManager<IdentityRole<Guid>> roleManager)
        {
            foreach (var roleName in Roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
                }
            }
        }
    }
}
