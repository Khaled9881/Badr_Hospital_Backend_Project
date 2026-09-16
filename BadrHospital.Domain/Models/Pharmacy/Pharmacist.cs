using BadrHospital.Domain.Models.Common;
using HospitalManagementSystem.Domain.Common;
using HospitalManagementSystem.Domain.Pharmacy;
using System;
using System.Collections.Generic;
using System.Text;

namespace BadrHospital.Domain.Models.Pharmacy
{
    public class Pharmacist : StaffMember
    {
        public string LicenseNumber { get; set; } = string.Empty;

        // Navigation
        public ICollection<Dispensing> Dispensings { get; set; } = new List<Dispensing>();
    }
}
