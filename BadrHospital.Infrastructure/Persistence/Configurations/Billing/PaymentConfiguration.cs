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
            builder.Property(x => x.PaymentMethod)
                            .HasConversion<string>()
                            .HasMaxLength(30)
                            .IsRequired();
            builder.Property(x => x.Status)
                            .HasConversion<string>()
                            .HasMaxLength(30)
                            .IsRequired();

            builder.HasIndex(p => p.InvoiceId);
        }
    }
}
