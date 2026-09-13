using HospitalManagementSystem.Domain.Common;

namespace HospitalManagementSystem.Domain.Lab
{
    public class LabResult : BaseEntity
    {
        public Guid LabOrderItemId { get; set; }
        public string Result { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public DateTime? CompletedAt { get; set; }
        public Guid TechnicianId { get; set; } // References the lab technician's ApplicationUser/Staff Id

        // Navigation
        public LabOrderItem LabOrderItem { get; set; } = null!;
    }
}
