using HospitalManagementSystem.Domain.Lab;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Infrastructure.Persistence.Configurations.Lab
{
    public class LabTestConfiguration : IEntityTypeConfiguration<LabTest>
    {
        public void Configure(EntityTypeBuilder<LabTest> builder)
        {
            builder.ToTable("LabTests");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name).IsRequired().HasMaxLength(200);
            builder.Property(t => t.Description).HasMaxLength(1000);
            builder.Property(t => t.Price).HasColumnType("decimal(18,2)");
            builder.Property(t => t.IsActive).HasDefaultValue(true);

            builder.HasIndex(t => t.Name).IsUnique();

            builder.HasQueryFilter(u => u.IsActive);


        }
    }
}
