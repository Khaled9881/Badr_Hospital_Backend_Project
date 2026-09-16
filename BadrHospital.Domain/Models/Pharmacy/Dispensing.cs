using BadrHospital.Domain.Models.Common;
using BadrHospital.Domain.Models.Pharmacy;
using HospitalManagementSystem.Domain.Common;

namespace HospitalManagementSystem.Domain.Pharmacy
{
    /// <summary>
    /// A dispensing event: a pharmacist fulfilling (all or part of) a Prescription.
    /// </summary>
    public class Dispensing : BaseEntity, IHasRowVersion
    {
        public Guid PrescriptionId { get; set; }
        public Guid PharmacistId { get; set; }

        public DateTime DispensedAt { get; set; } = DateTime.UtcNow;
        public string Notes { get; set; } = string.Empty;
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();

        // Navigation
        public Prescription Prescription { get; set; } = null!;
        public Pharmacist Pharmacist { get; set; } = null!;
        public ICollection<DispensingItem> DispensingItems { get; set; } = new List<DispensingItem>();
    }
}
