using HospitalManagementSystem.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Infrastructure.Persistence.Configurations.Identity
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRoles");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.RoleName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.CreatedAt)
                .IsRequired();

            builder.HasIndex(r => new { r.UserId, r.RoleName }).IsUnique();
        }
    }
}
