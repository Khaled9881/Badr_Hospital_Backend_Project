using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BadrHospital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDataIntegrityTriggers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE TRIGGER trg_Consultation_ValidatePatientDoctor
                ON Consultations
                AFTER INSERT, UPDATE
                AS
                BEGIN
                    IF EXISTS (
                        SELECT 1
                        FROM inserted i
                        JOIN Appointments a ON a.Id = i.AppointmentId
                        WHERE i.PatientId <> a.PatientId OR i.DoctorId <> a.DoctorId
                    )
                    BEGIN
                        RAISERROR('Consultation.PatientId/DoctorId must match the Appointment.', 16, 1);
                        ROLLBACK TRANSACTION;
                    END
                END;
            ");

            migrationBuilder.Sql(@"
                CREATE TRIGGER trg_LabOrder_ValidatePatientDoctor
                ON LabOrders
                AFTER INSERT, UPDATE
                AS
                BEGIN
                    IF EXISTS (
                        SELECT 1
                        FROM inserted i
                        JOIN Consultations c ON c.Id = i.ConsultationId
                        WHERE i.PatientId <> c.PatientId OR i.DoctorId <> c.DoctorId
                    )
                    BEGIN
                        RAISERROR('LabOrder.PatientId/DoctorId must match the Consultation.', 16, 1);
                        ROLLBACK TRANSACTION;
                    END
                END;
            ");

            migrationBuilder.Sql(@"
                CREATE TRIGGER trg_Prescription_ValidatePatientDoctor
                ON Prescriptions
                AFTER INSERT, UPDATE
                AS
                BEGIN
                    IF EXISTS (
                        SELECT 1
                        FROM inserted i
                        JOIN Consultations c ON c.Id = i.ConsultationId
                        WHERE i.PatientId <> c.PatientId OR i.DoctorId <> c.DoctorId
                    )
                    BEGIN
                        RAISERROR('Prescription.PatientId/DoctorId must match the Consultation.', 16, 1);
                        ROLLBACK TRANSACTION;
                    END
                END;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS trg_Consultation_ValidatePatientDoctor;");
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS trg_LabOrder_ValidatePatientDoctor;");
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS trg_Prescription_ValidatePatientDoctor;");
        }
    }
}
