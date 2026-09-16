using BadrHospital.Domain.Models.Lab;
using HospitalManagementSystem.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BadrHospital.Infrastructure.Persistence.Configurations.Lab
{
    public class LabTechnicianConfiguration : IEntityTypeConfiguration<LabTechnician>
    {
        public void Configure(EntityTypeBuilder<LabTechnician> builder)
        {
            builder.ToTable("LabTechnicians");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(t => t.LastName).IsRequired().HasMaxLength(100);
            builder.Property(t => t.CertificationNumber).IsRequired().HasMaxLength(50);
            builder.Property(t => t.PhoneNumber).HasMaxLength(20);
            builder.Property(t => t.Email).IsRequired().HasMaxLength(256);
            builder.Property(t => t.IsActive).HasDefaultValue(true);
            builder.Property(t => t.CreatedAt).IsRequired();

            builder.HasIndex(t => t.CertificationNumber).IsUnique();
            builder.HasIndex(t => t.Email).IsUnique();
            builder.HasIndex(t => t.ApplicationUserId)
                .IsUnique()
                .HasFilter("[ApplicationUserId] IS NOT NULL");

            builder.HasOne<ApplicationUser>()
                .WithOne(u => u.LabTechnician)
                .HasForeignKey<LabTechnician>(t => t.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(t => t.LabResults)
                .WithOne(r => r.Technician)
                .HasForeignKey(r => r.TechnicianId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
