using HospitalManagementSystem.Domain.Common;

namespace HospitalManagementSystem.Domain.Doctors
{
    /// <summary>
    /// One-to-many: a Doctor's weekly recurring availability slots.
    /// </summary>
    public class DoctorSchedule : BaseEntity
    {
        public Guid DoctorId { get; set; }
        public string DayOfWeek { get; set; } = string.Empty;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation
        public Doctor Doctor { get; set; } = null!;
    }
}
