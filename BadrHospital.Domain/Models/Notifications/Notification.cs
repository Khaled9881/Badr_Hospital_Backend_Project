using HospitalManagementSystem.Domain.Common;
using HospitalManagementSystem.Domain.Identity;

namespace HospitalManagementSystem.Domain.Notifications
{
    public class Notification : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Type { get; set; } = string.Empty; // e.g. Appointment, Prescription, LabResult, Billing
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ApplicationUser User { get; set; } = null!;
    }
}
