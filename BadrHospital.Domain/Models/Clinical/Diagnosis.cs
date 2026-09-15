using HospitalManagementSystem.Domain.Common;

namespace HospitalManagementSystem.Domain.Clinical
{
    /// <summary>
    /// One-to-many: a Consultation may record multiple diagnoses (e.g. ICD-10 codes).
    /// </summary>
    public class Diagnosis : BaseEntity, ISoftDelete
    {
        public Guid ConsultationId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsPrimary { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        // Navigation
        public Consultation Consultation { get; set; } = null!;
    }
}
