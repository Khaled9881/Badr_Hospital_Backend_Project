using HospitalManagementSystem.Domain.Departments;

namespace HospitalManagementSystem.Infrastructure.Persistence.Seeding
{
    /// <summary>
    /// Reference data for hospital departments. Runtime-seeded (not via
    /// HasData/migrations) so Ids are generated normally and Specializations
    /// can be linked up by Name after Departments are saved.
    /// </summary>
    public static class DepartmentSeedData
    {
        public static List<Department> GetSeedData()
        {
            return new List<Department>
            {
                new() { Name = "Cardiology", Description = "Diagnosis and treatment of heart and blood vessel conditions." },
                new() { Name = "Neurology", Description = "Diagnosis and treatment of disorders of the brain, spinal cord, and nervous system." },
                new() { Name = "Orthopedics", Description = "Diagnosis and treatment of conditions involving bones, joints, ligaments, and muscles." },
                new() { Name = "Pediatrics", Description = "Medical care for infants, children, and adolescents." },
                new() { Name = "Radiology", Description = "Diagnostic and interventional imaging services." },
                new() { Name = "Emergency Medicine", Description = "Immediate care for acute illnesses and injuries." },
                new() { Name = "General Surgery", Description = "Surgical treatment of a broad range of conditions." },
                new() { Name = "Internal Medicine", Description = "Prevention, diagnosis, and treatment of adult diseases." },
                new() { Name = "Dermatology", Description = "Diagnosis and treatment of skin, hair, and nail conditions." },
                new() { Name = "Psychiatry", Description = "Diagnosis and treatment of mental health and behavioral disorders." },
                new() { Name = "Obstetrics & Gynecology", Description = "Care related to pregnancy, childbirth, and the female reproductive system." },
                new() { Name = "Oncology", Description = "Diagnosis and treatment of cancer." },
            };
        }
    }
}
