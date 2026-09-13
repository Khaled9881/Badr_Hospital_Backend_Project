using HospitalManagementSystem.Domain.Common;
using HospitalManagementSystem.Domain.Doctors;

namespace HospitalManagementSystem.Domain.Departments
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Navigation
        public ICollection<DoctorDepartment> DoctorDepartments { get; set; } = new List<DoctorDepartment>();
        public ICollection<Specialization> Specializations { get; set; } = new List<Specialization>();
    }
}
