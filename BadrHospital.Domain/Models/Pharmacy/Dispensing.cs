using HospitalManagementSystem.Domain.Common;
using HospitalManagementSystem.Domain.Identity;

namespace HospitalManagementSystem.Domain.Pharmacy
{
    /// <summary>
    /// A dispensing event: a pharmacist fulfilling (all or part of) a Prescription.
    /// </summary>
    public class Dispensing : BaseEntity
    {
        public Guid PrescriptionId { get; set; }
        public Guid PharmacistId { get; set; } // References the pharmacist's ApplicationUser Id
        public DateTime DispensedAt { get; set; } = DateTime.UtcNow;
        public string Notes { get; set; } = string.Empty;

        // Navigation
        public Prescription Prescription { get; set; } = null!;
        public ApplicationUser Pharmacist { get; set; } = null!;
        public ICollection<DispensingItem> DispensingItems { get; set; } = new List<DispensingItem>();
    }
}
