using HospitalManagementSystem.Domain.Common;

namespace HospitalManagementSystem.Domain.Pharmacy
{
    public class PrescriptionItem : BaseEntity
    {
        public Guid PrescriptionId { get; set; }
        public Guid MedicineId { get; set; }
        public string Dosage { get; set; } = string.Empty;
        public string Frequency { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string Instructions { get; set; } = string.Empty;
        public int Quantity { get; set; }

        // Navigation
        public Prescription Prescription { get; set; } = null!;
        public Medicine Medicine { get; set; } = null!;
        public ICollection<DispensingItem> DispensingItems { get; set; } = new List<DispensingItem>();
    }
}
