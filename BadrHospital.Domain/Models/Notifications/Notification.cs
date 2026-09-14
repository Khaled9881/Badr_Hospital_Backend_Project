using BadrHospital.Domain.Enums;
using HospitalManagementSystem.Domain.Common;

namespace HospitalManagementSystem.Domain.Notifications
{
    public class Notification : BaseEntity
    {
        // Plain Guid FK to whichever user the app authenticates - no navigation
        // property, since the concrete user type (ApplicationUser) lives in
        // Infrastructure, not Domain. Relationship is configured from
        // ApplicationUserConfiguration on the Infrastructure side.
        public Guid UserId { get; set; }

        public NotificationType Type { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
