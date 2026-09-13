using HospitalManagementSystem.Domain.Pharmacy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Infrastructure.Persistence.Configurations.Pharmacy
{
    public class DispensingItemConfiguration : IEntityTypeConfiguration<DispensingItem>
    {
        public void Configure(EntityTypeBuilder<DispensingItem> builder)
        {
            builder.ToTable("DispensingItems");

            builder.HasKey(di => di.Id);

            builder.HasIndex(di => di.PrescriptionItemId);
            builder.HasIndex(di => di.MedicineBatchId);
        }
    }
}
