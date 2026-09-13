using HospitalManagementSystem.Domain.Pharmacy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Infrastructure.Persistence.Configurations.Pharmacy
{
    public class MedicineBatchConfiguration : IEntityTypeConfiguration<MedicineBatch>
    {
        public void Configure(EntityTypeBuilder<MedicineBatch> builder)
        {
            builder.ToTable("MedicineBatches");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.BatchNumber).IsRequired().HasMaxLength(50);
            builder.Property(b => b.ExpirationDate).IsRequired();
            builder.Property(b => b.PurchasePrice).HasColumnType("decimal(18,2)");

            builder.HasIndex(b => new { b.MedicineId, b.BatchNumber }).IsUnique();
            builder.HasIndex(b => b.ExpirationDate);

            // MedicineBatch -> DispensingItems: Restrict (preserve dispensing
            // audit trail even if a batch record is later removed)
            builder.HasMany(b => b.DispensingItems)
                .WithOne(di => di.MedicineBatch)
                .HasForeignKey(di => di.MedicineBatchId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
