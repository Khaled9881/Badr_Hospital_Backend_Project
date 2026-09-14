using BadrHospital.Domain.Enums;
using BadrHospital.Domain.Models.Common;
using HospitalManagementSystem.Domain.Common;

namespace HospitalManagementSystem.Domain.Billing
{
    /// <summary>
    /// One-to-many: an Invoice can be settled via multiple payments
    /// (e.g. partial payments, insurance + copay).
    /// </summary>
    public class Payment : BaseEntity, IHasRowVersion
    {
        public Guid InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime? PaidAt { get; set; }

        public byte[] RowVersion { get; set; } = Array.Empty<byte>();

        // Navigation
        public Invoice Invoice { get; set; } = null!;
    }
}
