# Data Integrity Triggers

This project enforces Patient/Doctor consistency in two layers, deliberately:

1. **Application layer** (primary) - `BusinessRuleException` checks in the
   relevant command handlers (`CreateConsultationCommandHandler`,
   `CreateLabOrderCommandHandler`, `CreatePrescriptionCommandHandler`) verify
   that `PatientId`/`DoctorId` match the parent record before saving. This is
   the main line of defense - it gives clear, catchable C# exceptions that
   `GlobalExceptionHandler` turns into proper 400 responses.

2. **Database triggers** (safety net) - these live directly in SQL Server,
   NOT in EF Core migrations or C# code, so they are easy to miss when
   reading the codebase. They exist to catch any write path that bypasses
   the application layer entirely (raw SQL, a future service, a bulk import
   script, manual DB edits).

## Triggers in this database

| Trigger | Table | Rule enforced |
|---|---|---|
| `trg_Consultation_ValidatePatientDoctor` | `Consultations` | `PatientId`/`DoctorId` must match the parent `Appointment` |
| `trg_LabOrder_ValidatePatientDoctor` | `LabOrders` | `PatientId`/`DoctorId` must match the parent `Consultation` |
| `trg_Prescription_ValidatePatientDoctor` | `Prescriptions` | `PatientId`/`DoctorId` must match the parent `Consultation` |

All three fire `AFTER INSERT, UPDATE` and `ROLLBACK TRANSACTION` with a
`RAISERROR` if a mismatch is found.

## Why redundant PatientId/DoctorId columns exist at all

`Consultation.PatientId`/`DoctorId` (and similarly on `LabOrder`/
`Prescription`) are technically derivable via
`Consultation -> Appointment -> Patient/Doctor`. They're kept as direct
columns for query performance (avoiding a 2-3 table join on common lookups
like "this patient's lab history"), at the cost of needing the consistency
enforcement documented here. This is a deliberate normalization trade-off,
not an oversight.

## Where the actual trigger SQL lives

Not checked into EF Core migrations - run manually against the target
database, or apply via a raw SQL deployment script. If you need them
version-controlled, wrap each `CREATE TRIGGER` statement in an EF Core
migration's `migrationBuilder.Sql("...")` instead and remove this note.
