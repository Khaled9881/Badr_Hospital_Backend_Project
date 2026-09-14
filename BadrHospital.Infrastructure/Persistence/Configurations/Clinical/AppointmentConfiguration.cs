using HospitalManagementSystem.Domain.Clinical;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Infrastructure.Persistence.Configurations.Clinical
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointments");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.AppointmentDateTime).IsRequired();
            builder.Property(a => a.EndDateTime).IsRequired();
            builder.Property(x => x.Status)
                            .HasConversion<string>()
                            .HasMaxLength(30)
                            .IsRequired();
            builder.Property(a => a.Reason).HasMaxLength(500);
            builder.Property(a => a.Notes).HasMaxLength(2000);
            builder.Property(a => a.CreatedAt).IsRequired();

            builder.HasIndex(a => a.PatientId);
            builder.HasIndex(a => a.DoctorId);
            builder.HasIndex(a => a.AppointmentDateTime);

            // Appointment -> Consultation (0..1, cascade: the consultation record
            // belongs entirely to this appointment)
            builder.HasOne(a => a.Consultation)
                .WithOne(c => c.Appointment)
                .HasForeignKey<Consultation>(c => c.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Patient/Doctor relationships are configured (as Restrict) from
            // PatientConfiguration / DoctorConfiguration to avoid duplicate config.
        }
    }
}
