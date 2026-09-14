using HospitalManagementSystem.Domain.Departments;

namespace HospitalManagementSystem.Infrastructure.Persistence.Seeding
{
    /// <summary>
    /// Reference data for specializations, grouped by department name.
    /// Must be seeded AFTER Departments (needs their generated Ids) -
    /// call GetSeedData(savedDepartments) with the already-persisted
    /// Department entities, matched here by Name.
    /// </summary>
    public static class SpecializationSeedData
    {
        private static readonly Dictionary<string, string[]> SpecializationsByDepartment = new()
        {
            ["Cardiology"] = new[] { "Interventional Cardiology", "Electrophysiology", "Heart Failure Management" },
            ["Neurology"] = new[] { "Stroke Medicine", "Epilepsy", "Movement Disorders" },
            ["Orthopedics"] = new[] { "Sports Medicine", "Joint Replacement", "Spine Surgery" },
            ["Pediatrics"] = new[] { "Neonatology", "Pediatric Cardiology", "Pediatric Endocrinology" },
            ["Radiology"] = new[] { "Diagnostic Radiology", "Interventional Radiology", "Nuclear Medicine" },
            ["Emergency Medicine"] = new[] { "Trauma Care", "Toxicology" },
            ["General Surgery"] = new[] { "Laparoscopic Surgery", "Colorectal Surgery", "Vascular Surgery" },
            ["Internal Medicine"] = new[] { "Endocrinology", "Gastroenterology", "Nephrology", "Pulmonology" },
            ["Dermatology"] = new[] { "Cosmetic Dermatology", "Dermatopathology" },
            ["Psychiatry"] = new[] { "Child & Adolescent Psychiatry", "Addiction Psychiatry" },
            ["Obstetrics & Gynecology"] = new[] { "Maternal-Fetal Medicine", "Reproductive Endocrinology" },
            ["Oncology"] = new[] { "Medical Oncology", "Radiation Oncology", "Surgical Oncology" },
        };

        public static List<Specialization> GetSeedData(IEnumerable<Department> savedDepartments)
        {
            var departmentsByName = savedDepartments.ToDictionary(d => d.Name, d => d.Id);
            var result = new List<Specialization>();

            foreach (var (departmentName, specializations) in SpecializationsByDepartment)
            {
                if (!departmentsByName.TryGetValue(departmentName, out var departmentId))
                    continue; // department wasn't in the saved set - skip rather than fail

                result.AddRange(specializations.Select(name => new Specialization
                {
                    Name = name,
                    Description = $"{name} within the {departmentName} department.",
                    DepartmentId = departmentId,
                }));
            }

            return result;
        }
    }
}
