using HospitalManagementSystem.Domain.Pharmacy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Infrastructure.Persistence.Configurations.Pharmacy
{
    public class DispensingConfiguration : IEntityTypeConfiguration<Dispensing>
    {
        public void Configure(EntityTypeBuilder<Dispensing> builder)
        {
            builder.ToTable("Dispensings");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.DispensedAt).IsRequired();
            builder.Property(d => d.Notes).HasMaxLength(1000);

            builder.HasIndex(d => d.PrescriptionId);
            builder.HasIndex(d => d.PharmacistId);

            // Dispensing -> DispensingItems (one-to-many, cascade)
            builder.HasMany(d => d.DispensingItems)
                .WithOne(di => di.Dispensing)
                .HasForeignKey(di => di.DispensingId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
