using BadrHospital.Domain.Models.Doctors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BadrHospital.Infrastructure.Persistence.Configurations.Doctors
{
    public class DoctorSpecializationConfiguration : IEntityTypeConfiguration<DoctorSpecialization>
    {
        public void Configure(EntityTypeBuilder<DoctorSpecialization> builder)
        {
            builder.ToTable("DoctorSpecializations");

            builder.HasKey(ds => ds.Id);

            builder.Property(ds => ds.CertifiedAt).IsRequired();

            // Prevent the same doctor being linked to the same specialization twice
            builder.HasIndex(ds => new { ds.DoctorId, ds.SpecializationId }).IsUnique();

            // Specialization side of the join (Doctor side is configured in
            // DoctorConfiguration, same pattern as DoctorDepartment).
            builder.HasOne(ds => ds.Specialization)
                .WithMany(s => s.DoctorSpecializations)
                .HasForeignKey(ds => ds.SpecializationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
