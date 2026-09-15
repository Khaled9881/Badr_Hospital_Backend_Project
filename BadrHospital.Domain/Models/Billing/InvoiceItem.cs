using HospitalManagementSystem.Domain.Clinical;
using HospitalManagementSystem.Domain.Common;
using HospitalManagementSystem.Domain.Lab;
using HospitalManagementSystem.Domain.Pharmacy;

namespace HospitalManagementSystem.Domain.Billing
{
    public class InvoiceItem : BaseEntity
    {
        public Guid InvoiceId { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        // ... inside the class, after InvoiceId:
        public Guid? ConsultationId { get; set; }
        public Guid? LabOrderItemId { get; set; }
        public Guid? PrescriptionItemId { get; set; }

        // Navigation
        public Consultation? Consultation { get; set; }
        public LabOrderItem? LabOrderItem { get; set; }
        public PrescriptionItem? PrescriptionItem { get; set; }

        // Navigation
        public Invoice Invoice { get; set; } = null!;
    }
}
