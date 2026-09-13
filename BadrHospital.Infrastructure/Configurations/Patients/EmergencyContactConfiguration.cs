using HospitalManagementSystem.Domain.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Infrastructure.Persistence.Configurations.Patients
{
    public class EmergencyContactConfiguration : IEntityTypeConfiguration<EmergencyContact>
    {
        public void Configure(EntityTypeBuilder<EmergencyContact> builder)
        {
            builder.ToTable("EmergencyContacts");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name).IsRequired().HasMaxLength(150);
            builder.Property(e => e.Relationship).HasMaxLength(50);
            builder.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(20);
            builder.Property(e => e.CreatedAt).IsRequired();

            builder.HasIndex(e => e.PatientId);
        }
    }
}
