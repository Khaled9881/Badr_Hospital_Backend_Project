using BadrHospital.Domain.Models.Doctors;
using BadrHospital.Domain.Models.Lab;
using BadrHospital.Domain.Models.Pharmacy;
using HospitalManagementSystem.Domain.Billing;
using HospitalManagementSystem.Domain.Clinical;
using HospitalManagementSystem.Domain.Departments;
using HospitalManagementSystem.Domain.Doctors;
using HospitalManagementSystem.Domain.Lab;
using HospitalManagementSystem.Domain.Notifications;
using HospitalManagementSystem.Domain.Patients;
using HospitalManagementSystem.Domain.Pharmacy;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BadrHospital.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        // Patients
        DbSet<Patient> Patients { get; }
        DbSet<EmergencyContact> EmergencyContacts { get; }

        // Doctors / Departments
        DbSet<Doctor> Doctors { get; }
        DbSet<DoctorDepartment> DoctorDepartments { get; }
        DbSet<DoctorSchedule> DoctorSchedules { get; }
        DbSet<Department> Departments { get; }
        DbSet<Specialization> Specializations { get; }
        DbSet<DoctorSpecialization> DoctorSpecializations { get; }

        // Clinical
        DbSet<Appointment> Appointments { get; }
        DbSet<Consultation> Consultations { get; }
        DbSet<Diagnosis> Diagnoses { get; }

        // Lab
        DbSet<LabTest> LabTests { get; }
        DbSet<LabOrder> LabOrders { get; }
        DbSet<LabOrderItem> LabOrderItems { get; }
        DbSet<LabResult> LabResults { get; }
        DbSet<LabTechnician> LabTechnicians { get; }

        // Billing
        DbSet<Invoice> Invoices { get; }
        DbSet<InvoiceItem> InvoiceItems { get; }
        DbSet<Payment> Payments { get; }

        // Notifications
        DbSet<Notification> Notifications { get; }

        // Pharmacy / Inventory
        DbSet<Pharmacist> Pharmacists { get; }
        DbSet<Medicine> Medicines { get; }
        DbSet<MedicineBatch> MedicineBatches { get; }
        DbSet<Prescription> Prescriptions { get; }
        DbSet<PrescriptionItem> PrescriptionItems { get; }
        DbSet<Dispensing> Dispensings { get; }
        DbSet<DispensingItem> DispensingItems { get; }

        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess = true, CancellationToken cancellationToken = default);
    }
}
