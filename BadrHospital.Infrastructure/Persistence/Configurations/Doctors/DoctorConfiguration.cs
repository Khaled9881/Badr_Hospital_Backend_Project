using HospitalManagementSystem.Domain.Doctors;
using HospitalManagementSystem.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Infrastructure.Persistence.Configurations.Doctors
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctors");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(d => d.LastName).IsRequired().HasMaxLength(100);
            builder.Property(d => d.LicenseNumber).IsRequired().HasMaxLength(50);
            builder.Property(d => d.Bio).HasMaxLength(2000);
            builder.Property(d => d.PhoneNumber).HasMaxLength(20);
            builder.Property(d => d.Email).IsRequired().HasMaxLength(256);
            builder.Property(d => d.IsActive).HasDefaultValue(true);
            builder.Property(d => d.CreatedAt).IsRequired();

            builder.HasIndex(d => d.LicenseNumber).IsUnique();
            builder.HasIndex(d => d.Email).IsUnique();

            // Doctor <-> ApplicationUser (0..1, optional portal login)
            builder.HasIndex(d => d.ApplicationUserId)
                .IsUnique()
                .HasFilter("[ApplicationUserId] IS NOT NULL");

            // No navigation on Doctor - see the same note in PatientConfiguration.
            builder.HasOne<ApplicationUser>()
                .WithOne(u => u.Doctor)
                .HasForeignKey<Doctor>(d => d.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Doctor -> DoctorSchedules (one-to-many, cascade: schedule dies with the doctor)
            builder.HasMany(d => d.Schedules)
                .WithOne(s => s.Doctor)
                .HasForeignKey(s => s.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);

            // Doctor -> DoctorDepartments (many-to-many join, cascade on this side)
            builder.HasMany(d => d.DoctorDepartments)
                .WithOne(dd => dd.Doctor)
                .HasForeignKey(dd => dd.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);

            // Cross-cutting clinical relations: Restrict to protect medical history
            builder.HasMany(d => d.Appointments)
                .WithOne(a => a.Doctor)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(d => d.Consultations)
                .WithOne(c => c.Doctor)
                .HasForeignKey(c => c.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(d => d.LabOrders)
                .WithOne(l => l.Doctor)
                .HasForeignKey(l => l.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(d => d.Prescriptions)
                .WithOne(p => p.Doctor)
                .HasForeignKey(p => p.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(u => u.IsActive);


        }
    }
}
