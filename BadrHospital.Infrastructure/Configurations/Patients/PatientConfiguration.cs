using HospitalManagementSystem.Domain.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Infrastructure.Persistence.Configurations.Patients
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.MedicalRecordNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Gender).HasMaxLength(20);
            builder.Property(p => p.PhoneNumber).HasMaxLength(20);
            builder.Property(p => p.Address).HasMaxLength(500);
            builder.Property(p => p.CreatedAt).IsRequired();

            builder.HasIndex(p => p.MedicalRecordNumber).IsUnique();

            // Patient <-> ApplicationUser (0..1, optional portal login)
            // Filtered so multiple patients with a NULL ApplicationUserId don't collide
            // (SQL Server treats a plain unique index as allowing only one NULL row).
            builder.HasIndex(p => p.ApplicationUserId)
                .IsUnique()
                .HasFilter("[ApplicationUserId] IS NOT NULL");

            builder.HasOne(p => p.ApplicationUser)
                .WithOne(u => u.Patient)
                .HasForeignKey<Patient>(p => p.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Patient -> EmergencyContacts (one-to-many, cascade: contacts die with the patient)
            builder.HasMany(p => p.EmergencyContacts)
                .WithOne(e => e.Patient)
                .HasForeignKey(e => e.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Cross-cutting clinical/financial relations: Restrict so a patient
            // record can't be silently wiped out along with its medical/billing history.
            builder.HasMany(p => p.Appointments)
                .WithOne(a => a.Patient)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Consultations)
                .WithOne(c => c.Patient)
                .HasForeignKey(c => c.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.LabOrders)
                .WithOne(l => l.Patient)
                .HasForeignKey(l => l.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Prescriptions)
                .WithOne(pr => pr.Patient)
                .HasForeignKey(pr => pr.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Invoices)
                .WithOne(i => i.Patient)
                .HasForeignKey(i => i.PatientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
