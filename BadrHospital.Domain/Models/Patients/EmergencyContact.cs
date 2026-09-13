using HospitalManagementSystem.Domain.Common;

namespace HospitalManagementSystem.Domain.Patients
{
    /// <summary>
    /// One-to-many: a Patient can have multiple emergency contacts.
    /// </summary>
    public class EmergencyContact : BaseEntity, ISoftDelete
    {
        public Guid PatientId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Relationship { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsPrimary { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        bool ISoftDelete.IsDeleted { get; set; }
        DateTime? ISoftDelete.DeletedAt { get; set; }

        // Navigation
        public Patient Patient { get; set; } = null!;
    }
}
