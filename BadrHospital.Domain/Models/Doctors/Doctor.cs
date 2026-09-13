using HospitalManagementSystem.Domain.Clinical;
using HospitalManagementSystem.Domain.Common;
using HospitalManagementSystem.Domain.Departments;
using HospitalManagementSystem.Domain.Identity;
using HospitalManagementSystem.Domain.Lab;
using HospitalManagementSystem.Domain.Pharmacy;

namespace HospitalManagementSystem.Domain.Doctors
{
    public class Doctor : BaseEntity
    {
        // FK - optional link to portal login (0..1 per ERD)
        public Guid? ApplicationUserId { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int YearsOfExperience { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ApplicationUser? ApplicationUser { get; set; }
        public ICollection<DoctorDepartment> DoctorDepartments { get; set; } = new List<DoctorDepartment>();
        public ICollection<DoctorSchedule> Schedules { get; set; } = new List<DoctorSchedule>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<Consultation> Consultations { get; set; } = new List<Consultation>();
        public ICollection<LabOrder> LabOrders { get; set; } = new List<LabOrder>();
        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    }
}
