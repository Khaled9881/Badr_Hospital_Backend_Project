using HospitalManagementSystem.Domain.Pharmacy;

namespace HospitalManagementSystem.Infrastructure.Persistence.Seeding
{
    /// <summary>
    /// Reference data for the medicine catalog. This seeds Medicine entries
    /// only (no MedicineBatch/stock records) - batches are operational
    /// inventory data, not reference data, so they're intentionally out of scope.
    /// </summary>
    public static class MedicineSeedData
    {
        public static List<Medicine> GetSeedData()
        {
            return new List<Medicine>
            {
                new() { Name = "Panadol", GenericName = "Paracetamol", DosageForm = "Tablet", Strength = "500mg", Manufacturer = "GSK" },
                new() { Name = "Brufen", GenericName = "Ibuprofen", DosageForm = "Tablet", Strength = "400mg", Manufacturer = "Abbott" },
                new() { Name = "Augmentin", GenericName = "Amoxicillin/Clavulanate", DosageForm = "Tablet", Strength = "625mg", Manufacturer = "GSK" },
                new() { Name = "Amoxil", GenericName = "Amoxicillin", DosageForm = "Capsule", Strength = "500mg", Manufacturer = "GSK" },
                new() { Name = "Ventolin", GenericName = "Salbutamol", DosageForm = "Inhaler", Strength = "100mcg/dose", Manufacturer = "GSK" },
                new() { Name = "Glucophage", GenericName = "Metformin", DosageForm = "Tablet", Strength = "500mg", Manufacturer = "Merck" },
                new() { Name = "Lipitor", GenericName = "Atorvastatin", DosageForm = "Tablet", Strength = "20mg", Manufacturer = "Pfizer" },
                new() { Name = "Norvasc", GenericName = "Amlodipine", DosageForm = "Tablet", Strength = "5mg", Manufacturer = "Pfizer" },
                new() { Name = "Nexium", GenericName = "Esomeprazole", DosageForm = "Capsule", Strength = "40mg", Manufacturer = "AstraZeneca" },
                new() { Name = "Zyrtec", GenericName = "Cetirizine", DosageForm = "Tablet", Strength = "10mg", Manufacturer = "UCB" },
                new() { Name = "Ceftriaxone", GenericName = "Ceftriaxone Sodium", DosageForm = "Injection", Strength = "1g/vial", Manufacturer = "Roche" },
                new() { Name = "Insulin Glargine", GenericName = "Insulin Glargine", DosageForm = "Injection", Strength = "100 units/mL", Manufacturer = "Sanofi" },
                new() { Name = "Losec", GenericName = "Omeprazole", DosageForm = "Capsule", Strength = "20mg", Manufacturer = "AstraZeneca" },
                new() { Name = "Panadol Syrup", GenericName = "Paracetamol", DosageForm = "Syrup", Strength = "120mg/5mL", Manufacturer = "GSK" },
                new() { Name = "Flagyl", GenericName = "Metronidazole", DosageForm = "Tablet", Strength = "400mg", Manufacturer = "Sanofi" },
            };
        }
    }
}
