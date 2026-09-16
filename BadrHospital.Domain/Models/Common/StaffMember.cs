using HospitalManagementSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BadrHospital.Domain.Models.Common
{
    public abstract class StaffMember : BaseEntity
    {
        public Guid? ApplicationUserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
