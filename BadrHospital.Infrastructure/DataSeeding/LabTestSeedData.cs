using HospitalManagementSystem.Domain.Lab;

namespace HospitalManagementSystem.Infrastructure.Persistence.Seeding
{
    /// <summary>
    /// Reference data for the lab test catalog. Prices are illustrative -
    /// adjust to your actual pricing before using in a real environment.
    /// </summary>
    public static class LabTestSeedData
    {
        public static List<LabTest> GetSeedData()
        {
            return new List<LabTest>
            {
                new() { Name = "Complete Blood Count (CBC)", Description = "Evaluates overall health and detects a range of disorders including anemia and infection.", Price = 25.00m },
                new() { Name = "Basic Metabolic Panel (BMP)", Description = "Measures glucose, calcium, and electrolyte/fluid balance.", Price = 30.00m },
                new() { Name = "Comprehensive Metabolic Panel (CMP)", Description = "BMP plus liver and kidney function tests.", Price = 45.00m },
                new() { Name = "Lipid Panel", Description = "Measures cholesterol and triglyceride levels.", Price = 35.00m },
                new() { Name = "Thyroid Stimulating Hormone (TSH)", Description = "Screens for thyroid function disorders.", Price = 40.00m },
                new() { Name = "Hemoglobin A1c", Description = "Measures average blood sugar levels over the past 2-3 months.", Price = 38.00m },
                new() { Name = "Urinalysis", Description = "Detects a range of disorders including urinary tract infections and kidney disease.", Price = 20.00m },
                new() { Name = "Liver Function Test (LFT)", Description = "Assesses liver health and function.", Price = 42.00m },
                new() { Name = "Coagulation Panel (PT/INR)", Description = "Measures how well blood clots.", Price = 33.00m },
                new() { Name = "C-Reactive Protein (CRP)", Description = "Detects inflammation in the body.", Price = 28.00m },
                new() { Name = "Blood Culture", Description = "Detects bacteria or fungi in the bloodstream.", Price = 55.00m },
                new() { Name = "COVID-19 PCR Test", Description = "Detects active SARS-CoV-2 infection.", Price = 60.00m },
                new() { Name = "Chest X-Ray Referral Panel", Description = "Pre-imaging bloodwork panel for chest X-ray referrals.", Price = 32.00m },
                new() { Name = "Vitamin D, 25-Hydroxy", Description = "Measures vitamin D levels in the blood.", Price = 37.00m },
                new() { Name = "Iron Studies", Description = "Evaluates iron levels, including ferritin and transferrin saturation.", Price = 41.00m },
            };
        }
    }
}
