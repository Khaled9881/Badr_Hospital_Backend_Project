using HospitalManagementSystem.Domain.Clinical;
using HospitalManagementSystem.Domain.Common;
using HospitalManagementSystem.Domain.Doctors;
using HospitalManagementSystem.Domain.Patients;

namespace HospitalManagementSystem.Domain.Pharmacy
{
    /// <summary>
    /// Business rule: a Prescription is immutable once issued (no edit) -
    /// enforce this in the application/service layer, not on the entity itself.
    /// </summary>
    public class Prescription : BaseEntity
    {
        public Guid ConsultationId { get; set; }
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public string Status { get; set; } = string.Empty; // e.g. Issued, PartiallyDispensed, Dispensed, Cancelled
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Notes { get; set; } = string.Empty;

        // Navigation
        public Consultation Consultation { get; set; } = null!;
        public Patient Patient { get; set; } = null!;
        public Doctor Doctor { get; set; } = null!;
        public ICollection<PrescriptionItem> PrescriptionItems { get; set; } = new List<PrescriptionItem>();
        public ICollection<Dispensing> Dispensings { get; set; } = new List<Dispensing>();
    }
}
