using BadrHospital.Domain.Models.Common;
using HospitalManagementSystem.Domain.Common;

namespace HospitalManagementSystem.Domain.Pharmacy
{
    /// <summary>
    /// Line item of a Dispensing event: how much of which PrescriptionItem
    /// was fulfilled from which MedicineBatch (supports FEFO/batch tracking).
    /// </summary>
    public class DispensingItem : BaseEntity, IHasRowVersion
    {
        public Guid DispensingId { get; set; }
        public Guid PrescriptionItemId { get; set; }
        public Guid MedicineBatchId { get; set; }
        public int Quantity { get; set; }

        public byte[] RowVersion { get; set; } = Array.Empty<byte>();

        // Navigation
        public Dispensing Dispensing { get; set; } = null!;
        public PrescriptionItem PrescriptionItem { get; set; } = null!;
        public MedicineBatch MedicineBatch { get; set; } = null!;
    }
}
