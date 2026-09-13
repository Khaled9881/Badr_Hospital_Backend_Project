using BadrHospital.Domain.Models.Common;
using HospitalManagementSystem.Domain.Common;

namespace HospitalManagementSystem.Domain.Pharmacy
{
    /// <summary>
    /// A dispensing event: a pharmacist fulfilling (all or part of) a Prescription.
    /// </summary>
    public class Dispensing : BaseEntity, IHasRowVersion
    {
        public Guid PrescriptionId { get; set; }

        // Plain Guid on purpose - references the pharmacist's ApplicationUser Id,
        // but Domain can't hold a navigation property to ApplicationUser
        // (that type lives in Infrastructure). Configured one-directionally
        // from DispensingConfiguration on the Infrastructure side.
        public Guid PharmacistId { get; set; }

        public DateTime DispensedAt { get; set; } = DateTime.UtcNow;
        public string Notes { get; set; } = string.Empty;

        public byte[] RowVersion { get; set; } = Array.Empty<byte>();

        // Navigation
        public Prescription Prescription { get; set; } = null!;
        public ICollection<DispensingItem> DispensingItems { get; set; } = new List<DispensingItem>();
    }
}
