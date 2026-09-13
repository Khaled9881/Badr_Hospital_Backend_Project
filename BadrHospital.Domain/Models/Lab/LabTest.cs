using HospitalManagementSystem.Domain.Common;

namespace HospitalManagementSystem.Domain.Lab
{
    /// <summary>
    /// Catalog of available lab tests (e.g. CBC, Lipid Panel).
    /// </summary>
    public class LabTest : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<LabOrderItem> LabOrderItems { get; set; } = new List<LabOrderItem>();
    }
}
