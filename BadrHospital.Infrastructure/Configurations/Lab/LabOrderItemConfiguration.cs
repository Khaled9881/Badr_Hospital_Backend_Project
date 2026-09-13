using HospitalManagementSystem.Domain.Lab;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Infrastructure.Persistence.Configurations.Lab
{
    public class LabOrderItemConfiguration : IEntityTypeConfiguration<LabOrderItem>
    {
        public void Configure(EntityTypeBuilder<LabOrderItem> builder)
        {
            builder.ToTable("LabOrderItems");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Notes).HasMaxLength(500);

            builder.HasIndex(i => i.LabTestId);

            // LabTest -> LabOrderItems: Restrict (don't want a catalog change to
            // cascade-delete historical order line items)
            builder.HasOne(i => i.LabTest)
                .WithMany(t => t.LabOrderItems)
                .HasForeignKey(i => i.LabTestId)
                .OnDelete(DeleteBehavior.Restrict);

            // LabOrderItem -> LabResult (0..1, cascade: result belongs to this item)
            builder.HasOne(i => i.LabResult)
                .WithOne(r => r.LabOrderItem)
                .HasForeignKey<LabResult>(r => r.LabOrderItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
