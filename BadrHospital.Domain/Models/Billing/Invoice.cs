using HospitalManagementSystem.Domain.Common;
using HospitalManagementSystem.Domain.Patients;

namespace HospitalManagementSystem.Domain.Billing
{
    public class Invoice : BaseEntity
    {
        public Guid PatientId { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = string.Empty; // e.g. Draft, Issued, Paid, Overdue, Cancelled
        public decimal TotalAmount { get; set; }

        // Navigation
        public Patient Patient { get; set; } = null!;
        public ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
