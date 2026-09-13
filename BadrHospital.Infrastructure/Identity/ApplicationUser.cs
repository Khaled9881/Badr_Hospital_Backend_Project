using HospitalManagementSystem.Domain.Doctors;
using HospitalManagementSystem.Domain.Notifications;
using HospitalManagementSystem.Domain.Patients;
using Microsoft.AspNetCore.Identity;

namespace HospitalManagementSystem.Infrastructure.Identity
{
    /// <summary>
    /// Lives in Infrastructure, NOT Domain. Identity is a framework/auth
    /// concern - Domain must stay free of any reference to
    /// Microsoft.AspNetCore.Identity so it can be swapped out without
    /// touching business entities.
    ///
    /// This class is allowed to reference Domain types (Patient, Doctor) -
    /// Infrastructure depending on Domain is the correct direction.
    /// Domain must NOT reference this class back (see Patient.ApplicationUserId /
    /// Doctor.ApplicationUserId / Notification.UserId - plain Guid FKs, no
    /// navigation property to ApplicationUser).
    ///
    /// Inherited for free from IdentityUser&lt;Guid&gt;: Id, UserName,
    /// NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash,
    /// SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed,
    /// TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount.
    /// </summary>
    public class ApplicationUser : IdentityUser<Guid>
    {
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginAt { get; set; }

        // Navigation - fine here since Infrastructure -> Domain is allowed.
        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
