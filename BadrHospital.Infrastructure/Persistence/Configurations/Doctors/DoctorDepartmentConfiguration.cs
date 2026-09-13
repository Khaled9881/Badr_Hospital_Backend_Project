using HospitalManagementSystem.Domain.Doctors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Infrastructure.Persistence.Configurations.Doctors
{
    public class DoctorDepartmentConfiguration : IEntityTypeConfiguration<DoctorDepartment>
    {
        public void Configure(EntityTypeBuilder<DoctorDepartment> builder)
        {
            builder.ToTable("DoctorDepartments");

            builder.HasKey(dd => dd.Id);

            builder.Property(dd => dd.JoinedAt).IsRequired();

            // Prevent the same doctor being linked to the same department twice
            builder.HasIndex(dd => new { dd.DoctorId, dd.DepartmentId }).IsUnique();

            // Department side of the join (Doctor side is configured in DoctorConfiguration)
            builder.HasOne(dd => dd.Department)
                .WithMany(dep => dep.DoctorDepartments)
                .HasForeignKey(dd => dd.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
