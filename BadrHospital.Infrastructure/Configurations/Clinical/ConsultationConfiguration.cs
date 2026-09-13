using HospitalManagementSystem.Domain.Clinical;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Infrastructure.Persistence.Configurations.Clinical
{
    public class ConsultationConfiguration : IEntityTypeConfiguration<Consultation>
    {
        public void Configure(EntityTypeBuilder<Consultation> builder)
        {
            builder.ToTable("Consultations");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.StartedAt).IsRequired();
            builder.Property(c => c.Symptoms).HasMaxLength(2000);
            builder.Property(c => c.Notes).HasMaxLength(2000);

            // One consultation per appointment
            builder.HasIndex(c => c.AppointmentId).IsUnique();
            builder.HasIndex(c => c.PatientId);
            builder.HasIndex(c => c.DoctorId);

            // Consultation -> Diagnoses (one-to-many, cascade)
            builder.HasMany(c => c.Diagnoses)
                .WithOne(d => d.Consultation)
                .HasForeignKey(d => d.ConsultationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Consultation -> LabOrders / Prescriptions: Restrict, these are
            // independent clinical/financial records kept even if the consultation
            // itself were ever removed.
            builder.HasMany(c => c.LabOrders)
                .WithOne(l => l.Consultation)
                .HasForeignKey(l => l.ConsultationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Prescriptions)
                .WithOne(p => p.Consultation)
                .HasForeignKey(p => p.ConsultationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Doctor/Patient FKs configured (Restrict) from their own configurations.
        }
    }
}
