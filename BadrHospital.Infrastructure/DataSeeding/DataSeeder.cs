using BadrHospital.Infrastructure.DataSeeding;
using BadrHospital.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagementSystem.Infrastructure.Persistence.Seeding
{
    /// <summary>
    /// Applies reference-data seeding for Departments, Specializations,
    /// LabTests, and Medicines only. Each set is seeded independently and
    /// skipped if that table already has rows, so it's safe to call on
    /// every startup.
    ///
    /// Departments are seeded (and saved) before Specializations, since
    /// Specialization.DepartmentId needs the real, DB-generated Department Ids.
    ///
    /// Call from Program.cs after building the app, e.g.:
    ///   using (var scope = app.Services.CreateScope())
    ///   {
    ///       var context = scope.ServiceProvider.GetRequiredService&lt;ApplicationDbContext&gt;();
    ///       await DataSeeder.SeedAsync(context);
    ///   }
    /// </summary>
    public static class DataSeeder
    {
        private static ApplicationDbContext? _context { get; set; }
        public static async Task SeedAsync(IServiceProvider services)
        {
            _context = services.GetRequiredService<ApplicationDbContext>();

            var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            await RoleSeedData.SeedAsync(roleManager);

            await IdentitySeeder.SeedRolesAndAdminAsync(services);


            await SeedDepartmentsAndSpecializationsAsync(_context);
            await SeedLabTestsAsync(_context);
            await SeedMedicinesAsync(_context);

        }

        private static async Task SeedDepartmentsAndSpecializationsAsync(ApplicationDbContext context)
        {
            if (!await context.Departments.AnyAsync())
            {
                context.Departments.AddRange(DepartmentSeedData.GetSeedData());
                await context.SaveChangesAsync();
            }

            if (!await context.Specializations.AnyAsync())
            {
                var savedDepartments = await context.Departments.ToListAsync();
                context.Specializations.AddRange(SpecializationSeedData.GetSeedData(savedDepartments));
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedLabTestsAsync(ApplicationDbContext context)
        {
            if (!await context.LabTests.AnyAsync())
            {
                context.LabTests.AddRange(LabTestSeedData.GetSeedData());
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedMedicinesAsync(ApplicationDbContext context)
        {
            if (!await context.Medicines.AnyAsync())
            {
                context.Medicines.AddRange(MedicineSeedData.GetSeedData());
                await context.SaveChangesAsync();
            }
        }
    }
}
