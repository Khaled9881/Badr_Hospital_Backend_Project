using HospitalManagementSystem.Domain.Common;

namespace HospitalManagementSystem.Domain.Pharmacy
{
    /// <summary>
    /// One-to-many: a Medicine can have multiple stock batches (different expiry/purchase price).
    /// </summary>
    public class MedicineBatch : BaseEntity
    {
        public Guid MedicineId { get; set; }
        public string BatchNumber { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }
        public int Quantity { get; set; }
        public decimal PurchasePrice { get; set; }

        // Navigation
        public Medicine Medicine { get; set; } = null!;
        public ICollection<DispensingItem> DispensingItems { get; set; } = new List<DispensingItem>();
    }
}
