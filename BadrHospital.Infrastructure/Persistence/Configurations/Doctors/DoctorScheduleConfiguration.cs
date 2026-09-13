using HospitalManagementSystem.Domain.Doctors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Infrastructure.Persistence.Configurations.Doctors
{
    public class DoctorScheduleConfiguration : IEntityTypeConfiguration<DoctorSchedule>
    {
        public void Configure(EntityTypeBuilder<DoctorSchedule> builder)
        {
            builder.ToTable("DoctorSchedules");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.DayOfWeek).IsRequired().HasMaxLength(20);
            builder.Property(s => s.IsActive).HasDefaultValue(true);

            builder.HasIndex(s => new { s.DoctorId, s.DayOfWeek, s.StartTime }).IsUnique();
        }
    }
}
