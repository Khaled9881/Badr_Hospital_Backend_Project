using HospitalManagementSystem.Domain.Pharmacy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Infrastructure.Persistence.Configurations.Pharmacy
{
    public class PrescriptionItemConfiguration : IEntityTypeConfiguration<PrescriptionItem>
    {
        public void Configure(EntityTypeBuilder<PrescriptionItem> builder)
        {
            builder.ToTable("PrescriptionItems");

            builder.HasKey(pi => pi.Id);

            builder.Property(pi => pi.Dosage).IsRequired().HasMaxLength(100);
            builder.Property(pi => pi.Frequency).HasMaxLength(100);
            builder.Property(pi => pi.Duration).HasMaxLength(100);
            builder.Property(pi => pi.Instructions).HasMaxLength(500);

            builder.HasIndex(pi => pi.MedicineId);

            // PrescriptionItem -> DispensingItems: Restrict (fulfilment audit
            // trail must remain even if the originating item is ever removed)
            builder.HasMany(pi => pi.DispensingItems)
                .WithOne(di => di.PrescriptionItem)
                .HasForeignKey(di => di.PrescriptionItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
