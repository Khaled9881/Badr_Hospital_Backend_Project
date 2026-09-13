using HospitalManagementSystem.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Infrastructure.Persistence.Configurations.Identity
{
    /// <summary>
    /// Configures ONLY the custom properties added on top of IdentityUser&lt;Guid&gt;.
    /// UserName/Email/PasswordHash/etc. and their indexes (on NormalizedUserName,
    /// NormalizedEmail) are already configured by IdentityDbContext's own
    /// model building - don't redeclare them here or you'll fight the base config.
    /// </summary>
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.IsActive)
                .HasDefaultValue(true);

            builder.Property(u => u.CreatedAt)
                .IsRequired();

            // ApplicationUser -> Notifications (one-to-many, cascade).
            // WithOne() has no expression because Notification (Domain) has no
            // navigation property back to ApplicationUser (Infrastructure) -
            // only the plain UserId FK.
            builder.HasMany(u => u.Notifications)
                .WithOne()
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ApplicationUser <-> Patient / Doctor (0..1, configured from the
            // dependent side - see PatientConfiguration / DoctorConfiguration).

            builder.HasQueryFilter(u => u.IsActive);

        }
    }
}
