using HospitalManagementSystem.Domain.Pharmacy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Infrastructure.Persistence.Configurations.Pharmacy
{
    public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.ToTable("Prescriptions");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Status).IsRequired().HasMaxLength(30);
            builder.Property(p => p.CreatedAt).IsRequired();
            builder.Property(p => p.Notes).HasMaxLength(1000);

            builder.HasIndex(p => p.PatientId);
            builder.HasIndex(p => p.DoctorId);
            builder.HasIndex(p => p.ConsultationId);

            // Prescription -> PrescriptionItems (one-to-many, cascade)
            builder.HasMany(p => p.PrescriptionItems)
                .WithOne(pi => pi.Prescription)
                .HasForeignKey(pi => pi.PrescriptionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Prescription -> Dispensings: Restrict (dispensing history must
            // survive independently of the prescription record; the business
            // rule that prescriptions are immutable once issued is enforced
            // in the application/service layer, not here)
            builder.HasMany(p => p.Dispensings)
                .WithOne(d => d.Prescription)
                .HasForeignKey(d => d.PrescriptionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
