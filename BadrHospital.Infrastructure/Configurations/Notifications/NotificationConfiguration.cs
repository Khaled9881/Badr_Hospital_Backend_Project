using HospitalManagementSystem.Domain.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Infrastructure.Persistence.Configurations.Notifications
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications");

            builder.HasKey(n => n.Id);

            builder.Property(n => n.Type).IsRequired().HasMaxLength(50);
            builder.Property(n => n.Title).IsRequired().HasMaxLength(200);
            builder.Property(n => n.Message).IsRequired().HasMaxLength(2000);
            builder.Property(n => n.IsRead).HasDefaultValue(false);
            builder.Property(n => n.CreatedAt).IsRequired();

            builder.HasIndex(n => new { n.UserId, n.IsRead });
        }
    }
}
