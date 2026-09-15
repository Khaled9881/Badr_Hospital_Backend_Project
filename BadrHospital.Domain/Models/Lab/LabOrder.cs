using BadrHospital.Domain.Enums;
using HospitalManagementSystem.Domain.Clinical;
using HospitalManagementSystem.Domain.Common;
using HospitalManagementSystem.Domain.Doctors;
using HospitalManagementSystem.Domain.Patients;

namespace HospitalManagementSystem.Domain.Lab
{
    public class LabOrder : BaseEntity, ISoftDelete
    {
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid ConsultationId { get; set; }
        public LabOrderStatus Status { get; set; }
        public DateTime OrderedAt { get; set; } = DateTime.UtcNow;
        public string Notes { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        // Navigation
        public Patient Patient { get; set; } = null!;
        public Doctor Doctor { get; set; } = null!;
        public Consultation Consultation { get; set; } = null!;
        public ICollection<LabOrderItem> LabOrderItems { get; set; } = new List<LabOrderItem>();
    }
}
