using HospitalManagementSystem.Domain.Common;
using HospitalManagementSystem.Domain.Doctors;
using HospitalManagementSystem.Domain.Notifications;
using HospitalManagementSystem.Domain.Patients;

namespace HospitalManagementSystem.Domain.Identity
{
    /// <summary>
    /// Core login/account entity. A Patient or Doctor optionally links back to one
    /// ApplicationUser (0..1) - not every patient/doctor necessarily has portal access.
    /// </summary>
    public class ApplicationUser : BaseEntity
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string SecurityStamp { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginAt { get; set; }

        // Navigation
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
