using HospitalManagementSystem.Domain.Common;
using HospitalManagementSystem.Domain.Doctors;
using HospitalManagementSystem.Domain.Lab;
using HospitalManagementSystem.Domain.Patients;
using HospitalManagementSystem.Domain.Pharmacy;

namespace HospitalManagementSystem.Domain.Clinical
{
    public class Consultation : BaseEntity, ISoftDelete
    {
        public Guid AppointmentId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string Symptoms { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        // Navigation
        public Appointment Appointment { get; set; } = null!;
        public Doctor Doctor { get; set; } = null!;
        public Patient Patient { get; set; } = null!;
        public ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();
        public ICollection<LabOrder> LabOrders { get; set; } = new List<LabOrder>();
        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    }
}
