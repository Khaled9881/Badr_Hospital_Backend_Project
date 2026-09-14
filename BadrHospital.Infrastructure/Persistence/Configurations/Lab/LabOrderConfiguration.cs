using HospitalManagementSystem.Domain.Lab;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Infrastructure.Persistence.Configurations.Lab
{
    public class LabOrderConfiguration : IEntityTypeConfiguration<LabOrder>
    {
        public void Configure(EntityTypeBuilder<LabOrder> builder)
        {
            builder.ToTable("LabOrders");

            builder.HasKey(o => o.Id);

            builder.Property(x => x.Status)
                            .HasConversion<string>()
                            .HasMaxLength(30)
                            .IsRequired();
            builder.Property(o => o.OrderedAt).IsRequired();
            builder.Property(o => o.Notes).HasMaxLength(1000);

            builder.HasIndex(o => o.PatientId);
            builder.HasIndex(o => o.DoctorId);
            builder.HasIndex(o => o.ConsultationId);

            // LabOrder -> LabOrderItems (one-to-many, cascade)
            builder.HasMany(o => o.LabOrderItems)
                .WithOne(i => i.LabOrder)
                .HasForeignKey(i => i.LabOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Patient/Doctor/Consultation FKs are Restrict (configured on their own sides).
        }
    }
}
