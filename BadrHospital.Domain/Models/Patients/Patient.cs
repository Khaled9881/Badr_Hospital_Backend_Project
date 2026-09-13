using HospitalManagementSystem.Domain.Billing;
using HospitalManagementSystem.Domain.Clinical;
using HospitalManagementSystem.Domain.Common;
using HospitalManagementSystem.Domain.Lab;
using HospitalManagementSystem.Domain.Pharmacy;

namespace HospitalManagementSystem.Domain.Patients
{
    public class Patient : BaseEntity, ISoftDelete
    {
        // FK - optional link to portal login (0..1 per ERD).
        // Plain Guid on purpose: Domain must not reference the Infrastructure-layer
        // ApplicationUser type. No navigation property here - the relationship
        // is configured from the Infrastructure side (PatientConfiguration).
        public Guid? ApplicationUserId { get; set; }

        public string MedicalRecordNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ICollection<EmergencyContact> EmergencyContacts { get; set; } = new List<EmergencyContact>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<Consultation> Consultations { get; set; } = new List<Consultation>();
        public ICollection<LabOrder> LabOrders { get; set; } = new List<LabOrder>();
        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
        bool ISoftDelete.IsDeleted { get; set; }
        DateTime? ISoftDelete.DeletedAt { get; set; }
    }
}
