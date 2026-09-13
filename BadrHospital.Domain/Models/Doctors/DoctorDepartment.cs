using HospitalManagementSystem.Domain.Common;
using HospitalManagementSystem.Domain.Departments;

namespace HospitalManagementSystem.Domain.Doctors
{
    /// <summary>
    /// Join entity resolving the Doctor &lt;-&gt; Department many-to-many relationship.
    /// </summary>
    public class DoctorDepartment : BaseEntity, ISoftDelete
    {
        public Guid DoctorId { get; set; }
        public Guid DepartmentId { get; set; }
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
        public bool IsPrimary { get; set; }

        bool ISoftDelete.IsDeleted { get; set; }
        DateTime? ISoftDelete.DeletedAt { get; set; }

        // Navigation
        public Doctor Doctor { get; set; } = null!;
        public Department Department { get; set; } = null!;
    }
}
