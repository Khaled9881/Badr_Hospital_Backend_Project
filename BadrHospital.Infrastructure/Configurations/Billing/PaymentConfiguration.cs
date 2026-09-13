using HospitalManagementSystem.Domain.Billing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Infrastructure.Persistence.Configurations.Billing
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Amount).HasColumnType("decimal(18,2)");
            builder.Property(p => p.PaymentMethod).IsRequired().HasMaxLength(30);
            builder.Property(p => p.Status).IsRequired().HasMaxLength(30);

            builder.HasIndex(p => p.InvoiceId);
        }
    }
}
