using HospitalManagementSystem.Domain.Clinical;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Infrastructure.Persistence.Configurations.Clinical
{
    public class DiagnosisConfiguration : IEntityTypeConfiguration<Diagnosis>
    {
        public void Configure(EntityTypeBuilder<Diagnosis> builder)
        {
            builder.ToTable("Diagnoses");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Code).IsRequired().HasMaxLength(20);
            builder.Property(d => d.Name).IsRequired().HasMaxLength(200);
            builder.Property(d => d.Description).HasMaxLength(1000);

            builder.HasIndex(d => d.ConsultationId);
        }
    }
}
