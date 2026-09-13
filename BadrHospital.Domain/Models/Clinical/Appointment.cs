using BadrHospital.Domain.Models.Common;
using HospitalManagementSystem.Domain.Common;
using HospitalManagementSystem.Domain.Doctors;
using HospitalManagementSystem.Domain.Patients;

namespace HospitalManagementSystem.Domain.Clinical
{
    public class Appointment : BaseEntity, ISoftDelete, IHasRowVersion
    {
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Status { get; set; } = string.Empty; // e.g. Scheduled, Completed, Cancelled, NoShow
        public string Reason { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        bool ISoftDelete.IsDeleted { get; set; }
        DateTime? ISoftDelete.DeletedAt { get; set; }

        public byte[] RowVersion { get; set; } = Array.Empty<byte>();


        // Navigation
        public Patient Patient { get; set; } = null!;
        public Doctor Doctor { get; set; } = null!;
        public Consultation? Consultation { get; set; } // 0..1 per ERD
    }
}
