using HospitalManagementSystem.Domain.Billing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Infrastructure.Persistence.Configurations.Billing
{
    public class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
    {
        public void Configure(EntityTypeBuilder<InvoiceItem> builder)
        {
            builder.ToTable("InvoiceItems");

            builder.HasKey(ii => ii.Id);

            builder.Property(ii => ii.Description).IsRequired().HasMaxLength(500);
            builder.Property(ii => ii.UnitPrice).HasColumnType("decimal(18,2)");
            builder.Property(ii => ii.TotalPrice).HasColumnType("decimal(18,2)");

            builder.HasOne(ii => ii.Consultation)
                    .WithMany()
                    .HasForeignKey(ii => ii.ConsultationId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ii => ii.LabOrderItem)
                .WithMany()
                .HasForeignKey(ii => ii.LabOrderItemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ii => ii.PrescriptionItem)
                .WithMany()
                .HasForeignKey(ii => ii.PrescriptionItemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable(t => t.HasCheckConstraint(
                    "CK_InvoiceItem_AtMostOneSource",
                    "(CASE WHEN [ConsultationId] IS NOT NULL THEN 1 ELSE 0 END + " +
                    "CASE WHEN [LabOrderItemId] IS NOT NULL THEN 1 ELSE 0 END + " +
                    "CASE WHEN [PrescriptionItemId] IS NOT NULL THEN 1 ELSE 0 END) <= 1"));
        }
    }
}
