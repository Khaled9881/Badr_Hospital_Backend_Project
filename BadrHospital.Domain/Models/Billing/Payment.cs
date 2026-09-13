using HospitalManagementSystem.Domain.Common;

namespace HospitalManagementSystem.Domain.Billing
{
    /// <summary>
    /// One-to-many: an Invoice can be settled via multiple payments
    /// (e.g. partial payments, insurance + copay).
    /// </summary>
    public class Payment : BaseEntity
    {
        public Guid InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty; // e.g. Cash, Card, Insurance
        public string Status { get; set; } = string.Empty; // e.g. Pending, Completed, Failed, Refunded
        public DateTime? PaidAt { get; set; }

        // Navigation
        public Invoice Invoice { get; set; } = null!;
    }
}
