using HospitalManagementSystem.Domain.Billing;
using HospitalManagementSystem.Domain.Clinical;
using HospitalManagementSystem.Domain.Departments;
using HospitalManagementSystem.Domain.Doctors;
using HospitalManagementSystem.Domain.Lab;
using HospitalManagementSystem.Domain.Notifications;
using HospitalManagementSystem.Domain.Patients;
using HospitalManagementSystem.Domain.Pharmacy;
using HospitalManagementSystem.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HospitalManagementSystem.Infrastructure.Persistence
{
    /// <summary>
    /// Inherits IdentityDbContext, which brings in (and manages the schema for)
    /// ApplicationUser, IdentityRole&lt;Guid&gt;, and the join/claim/login/token
    /// tables Identity needs (UserRoles, UserClaims, UserLogins, UserTokens,
    /// RoleClaims). No custom UserRole entity is needed anymore.
    /// </summary>
    public class ApplicationDbContext
        : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Patients
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<EmergencyContact> EmergencyContacts => Set<EmergencyContact>();

        // Doctors / Departments
        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<DoctorDepartment> DoctorDepartments => Set<DoctorDepartment>();
        public DbSet<DoctorSchedule> DoctorSchedules => Set<DoctorSchedule>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Specialization> Specializations => Set<Specialization>();

        // Clinical
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<Consultation> Consultations => Set<Consultation>();
        public DbSet<Diagnosis> Diagnoses => Set<Diagnosis>();

        // Lab
        public DbSet<LabTest> LabTests => Set<LabTest>();
        public DbSet<LabOrder> LabOrders => Set<LabOrder>();
        public DbSet<LabOrderItem> LabOrderItems => Set<LabOrderItem>();
        public DbSet<LabResult> LabResults => Set<LabResult>();

        // Billing
        public DbSet<Invoice> Invoices => Set<Invoice>();
        public DbSet<InvoiceItem> InvoiceItems => Set<InvoiceItem>();
        public DbSet<Payment> Payments => Set<Payment>();

        // Notifications
        public DbSet<Notification> Notifications => Set<Notification>();

        // Pharmacy / Inventory
        public DbSet<Medicine> Medicines => Set<Medicine>();
        public DbSet<MedicineBatch> MedicineBatches => Set<MedicineBatch>();
        public DbSet<Prescription> Prescriptions => Set<Prescription>();
        public DbSet<PrescriptionItem> PrescriptionItems => Set<PrescriptionItem>();
        public DbSet<Dispensing> Dispensings => Set<Dispensing>();
        public DbSet<DispensingItem> DispensingItems => Set<DispensingItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // MUST run first: builds the Identity schema (AspNetUsers, AspNetRoles,
            // AspNetUserRoles, AspNetUserClaims, AspNetUserLogins, AspNetUserTokens,
            // AspNetRoleClaims). Renaming those tables (optional, cosmetic) has to
            // happen AFTER this call, or Identity's own conventions overwrite it.
            base.OnModelCreating(modelBuilder);

            // Picks up every IEntityTypeConfiguration<T> in this assembly -
            // add new configs under Configurations/ and they're applied automatically.
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Optional: rename the default AspNet* tables to match your ERD's
            // naming style. Purely cosmetic - remove this block to keep defaults.
            modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUsers");
            modelBuilder.Entity<IdentityRole<Guid>>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
        }
    }
}
