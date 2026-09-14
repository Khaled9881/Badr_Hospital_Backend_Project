using HospitalManagementSystem.Domain.Common;
using HospitalManagementSystem.Domain.Departments;
using HospitalManagementSystem.Domain.Doctors;
using System;
using System.Collections.Generic;
using System.Text;

namespace BadrHospital.Domain.Models.Doctors
{
    public class DoctorSpecialization : BaseEntity
    {
        public Guid DoctorId { get; set; }
        public Guid SpecializationId { get; set; }
        public DateTime CertifiedAt { get; set; } = DateTime.UtcNow;
        public bool IsPrimary { get; set; }

        // Navigation
        public Doctor Doctor { get; set; } = null!;
        public Specialization Specialization { get; set; } = null!;
    }
}
