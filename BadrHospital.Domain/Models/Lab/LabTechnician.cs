using BadrHospital.Domain.Models.Common;
using HospitalManagementSystem.Domain.Common;
using HospitalManagementSystem.Domain.Lab;
using System;
using System.Collections.Generic;
using System.Text;

namespace BadrHospital.Domain.Models.Lab
{
    public class LabTechnician : StaffMember
    {
        public string CertificationNumber { get; set; } = string.Empty;

        // Navigation
        public ICollection<LabResult> LabResults { get; set; } = new List<LabResult>();
    }
}
