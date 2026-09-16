using BadrHospital.Domain.Models.Pharmacy;
using HospitalManagementSystem.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BadrHospital.Infrastructure.Persistence.Configurations.Pharmacy
{
    public class PharmacistConfiguration : IEntityTypeConfiguration<Pharmacist>
    {
        public void Configure(EntityTypeBuilder<Pharmacist> builder)
        {
            builder.ToTable("Pharmacists");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.LicenseNumber).IsRequired().HasMaxLength(50);
            builder.Property(p => p.PhoneNumber).HasMaxLength(20);
            builder.Property(p => p.Email).IsRequired().HasMaxLength(256);
            builder.Property(p => p.IsActive).HasDefaultValue(true);
            builder.Property(p => p.CreatedAt).IsRequired();

            builder.HasIndex(p => p.LicenseNumber).IsUnique();
            builder.HasIndex(p => p.Email).IsUnique();
            builder.HasIndex(p => p.ApplicationUserId)
                .IsUnique()
                .HasFilter("[ApplicationUserId] IS NOT NULL");

            builder.HasOne<ApplicationUser>()
                .WithOne(u => u.Pharmacist)
                .HasForeignKey<Pharmacist>(p => p.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Dispensings)
                .WithOne(d => d.Pharmacist)
                .HasForeignKey(d => d.PharmacistId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
