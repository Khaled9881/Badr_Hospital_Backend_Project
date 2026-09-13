using HospitalManagementSystem.Domain.Common;
using HospitalManagementSystem.Domain.Doctors;

namespace HospitalManagementSystem.Domain.Departments
{
    public class Department : BaseEntity, ISoftDelete
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        bool ISoftDelete.IsDeleted { get; set; }
        DateTime? ISoftDelete.DeletedAt { get; set; }

        // Navigation
        public ICollection<DoctorDepartment> DoctorDepartments { get; set; } = new List<DoctorDepartment>();
        public ICollection<Specialization> Specializations { get; set; } = new List<Specialization>();
    }
}
