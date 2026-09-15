using HospitalManagementSystem.Domain.Common;

namespace HospitalManagementSystem.Domain.Lab
{
    /// <summary>
    /// Line item of a LabOrder, referencing a specific LabTest.
    /// Has a 0..1 relationship to LabResult (result may not exist yet).
    /// </summary>
    public class LabOrderItem : BaseEntity, ISoftDelete
    {
        public Guid LabOrderId { get; set; }
        public Guid LabTestId { get; set; }
        public string Notes { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        // Navigation
        public LabOrder LabOrder { get; set; } = null!;
        public LabTest LabTest { get; set; } = null!;
        public LabResult? LabResult { get; set; }
    }
}
