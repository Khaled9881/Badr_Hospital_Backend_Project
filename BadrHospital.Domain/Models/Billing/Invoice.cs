using BadrHospital.Domain.Enums;
using BadrHospital.Domain.Models.Common;
using HospitalManagementSystem.Domain.Common;
using HospitalManagementSystem.Domain.Patients;

namespace HospitalManagementSystem.Domain.Billing
{
    public class Invoice : BaseEntity, IHasRowVersion
    {
        public Guid PatientId { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public InvoiceStatus Status { get; set; }
        public decimal TotalAmount { get; set; }

        public byte[] RowVersion { get; set; } = Array.Empty<byte>();

        // Navigation
        public Patient Patient { get; set; } = null!;
        public ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
