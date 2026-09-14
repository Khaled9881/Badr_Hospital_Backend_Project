using BadrHospital.Domain.Models.Common;
using HospitalManagementSystem.Domain.Common;

namespace HospitalManagementSystem.Domain.Doctors
{
    /// <summary>
    /// One-to-many: a Doctor's weekly recurring availability slots.
    /// </summary>
    public class DoctorSchedule : BaseEntity, IHasRowVersion
    {
        public Guid DoctorId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation
        public Doctor Doctor { get; set; } = null!;
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    }
}
