using HospitalManagementSystem.Domain.Lab;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Infrastructure.Persistence.Configurations.Lab
{
    public class LabResultConfiguration : IEntityTypeConfiguration<LabResult>
    {
        public void Configure(EntityTypeBuilder<LabResult> builder)
        {
            builder.ToTable("LabResults");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Result).IsRequired().HasMaxLength(2000);
            builder.Property(r => r.Notes).HasMaxLength(1000);

            builder.HasIndex(r => r.LabOrderItemId).IsUnique();

            // TechnicianId intentionally has no FK/navigation - it's a loose
            // reference to whichever staff table/service owns technician identity.
        }
    }
}
