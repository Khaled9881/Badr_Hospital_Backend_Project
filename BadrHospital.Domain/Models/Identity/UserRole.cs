using HospitalManagementSystem.Domain.Common;

namespace HospitalManagementSystem.Domain.Identity
{
    /// <summary>
    /// One-to-many: a single ApplicationUser can carry multiple roles (e.g. "Doctor", "Admin").
    /// </summary>
    public class UserRole : BaseEntity
    {
        public Guid UserId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ApplicationUser User { get; set; } = null!;
    }
}
