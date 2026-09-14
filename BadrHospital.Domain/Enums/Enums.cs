using System;
using System.Collections.Generic;
using System.Text;

namespace BadrHospital.Domain.Enums;

public enum Gender
{
    Male = 1,
    Female = 2
}

public enum AppointmentStatus
{
    Pending = 1,
    Confirmed = 2,
    InProgress = 3,
    Completed = 4,
    Cancelled = 5,
    NoShow = 6
}

public enum PrescriptionStatus
{
    Draft = 1,
    Issued = 2,
    PartiallyDispensed = 3,
    FullyDispensed = 4,
    Cancelled = 5
}


public enum LabOrderStatus
{
    Requested = 1,
    SampleCollected = 2,
    Processing = 3,
    Completed = 4,
    Cancelled = 5
}

public enum InvoiceStatus
{
    Pending = 1,
    PartiallyPaid = 2,
    Paid = 3,
    Cancelled = 4
}

public enum PaymentMethod
{
    Cash = 1,
    Card = 2,
    Online = 3,
    Insurance = 4
}

public enum PaymentStatus
{
    Pending = 1,
    Completed = 2,
    Failed = 3,
    Refunded = 4
}

public enum NotificationType
{
    AppointmentReminder = 1,
    AppointmentCancelled = 2,
    LabResultAvailable = 3,
    PrescriptionReady = 4,
    PaymentReceived = 5
}


