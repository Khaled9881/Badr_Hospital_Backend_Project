using HospitalManagementSystem.Domain.Pharmacy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Infrastructure.Persistence.Configurations.Pharmacy
{
    public class MedicineConfiguration : IEntityTypeConfiguration<Medicine>
    {
        public void Configure(EntityTypeBuilder<Medicine> builder)
        {
            builder.ToTable("Medicines");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Name).IsRequired().HasMaxLength(200);
            builder.Property(m => m.GenericName).HasMaxLength(200);
            builder.Property(m => m.DosageForm).HasMaxLength(50);
            builder.Property(m => m.Strength).HasMaxLength(50);
            builder.Property(m => m.Manufacturer).HasMaxLength(200);

            builder.HasIndex(m => m.Name);

            // Medicine -> MedicineBatches (one-to-many, cascade: batches are
            // owned stock records of this medicine)
            builder.HasMany(m => m.Batches)
                .WithOne(b => b.Medicine)
                .HasForeignKey(b => b.MedicineId)
                .OnDelete(DeleteBehavior.Cascade);

            // Medicine -> PrescriptionItems: Restrict (never wipe historical
            // prescriptions if a medicine catalog entry is removed)
            builder.HasMany(m => m.PrescriptionItems)
                .WithOne(pi => pi.Medicine)
                .HasForeignKey(pi => pi.MedicineId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
