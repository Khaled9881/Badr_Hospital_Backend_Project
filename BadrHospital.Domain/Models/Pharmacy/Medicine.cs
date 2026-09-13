using HospitalManagementSystem.Domain.Common;

namespace HospitalManagementSystem.Domain.Pharmacy
{
    /// <summary>
    /// Catalog entry for a medicine (drug), independent of stock/batches.
    /// </summary>
    public class Medicine : BaseEntity, ISoftDelete
    {
        public string Name { get; set; } = string.Empty;
        public string GenericName { get; set; } = string.Empty;
        public string DosageForm { get; set; } = string.Empty; // e.g. Tablet, Syrup, Injection
        public string Strength { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;

        bool ISoftDelete.IsDeleted { get; set; }
        DateTime? ISoftDelete.DeletedAt { get; set; }

        // Navigation
        public ICollection<MedicineBatch> Batches { get; set; } = new List<MedicineBatch>();
        public ICollection<PrescriptionItem> PrescriptionItems { get; set; } = new List<PrescriptionItem>();
    }
}
