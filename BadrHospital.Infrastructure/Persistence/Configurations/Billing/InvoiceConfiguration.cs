using HospitalManagementSystem.Domain.Billing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Infrastructure.Persistence.Configurations.Billing
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoices");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.InvoiceNumber).IsRequired().HasMaxLength(50);
            builder.Property(i => i.CreatedAt).IsRequired();
            builder.Property(x => x.Status)
                            .HasConversion<string>()
                            .HasMaxLength(30)
                            .IsRequired();
            builder.Property(i => i.TotalAmount).HasColumnType("decimal(18,2)");

            builder.HasIndex(i => i.InvoiceNumber).IsUnique();
            builder.HasIndex(i => i.PatientId);

            // Invoice -> InvoiceItems (one-to-many, cascade)
            builder.HasMany(i => i.InvoiceItems)
                .WithOne(ii => ii.Invoice)
                .HasForeignKey(ii => ii.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Invoice -> Payments (one-to-many, cascade)
            builder.HasMany(i => i.Payments)
                .WithOne(p => p.Invoice)
                .HasForeignKey(p => p.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
