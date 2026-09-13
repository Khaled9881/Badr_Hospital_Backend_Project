using HospitalManagementSystem.Domain.Common;

namespace HospitalManagementSystem.Domain.Departments
{
    /// <summary>
    /// One-to-many: a Department can offer multiple specializations.
    /// </summary>
    public class Specialization : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid DepartmentId { get; set; }

        // Navigation
        public Department Department { get; set; } = null!;
    }
}
