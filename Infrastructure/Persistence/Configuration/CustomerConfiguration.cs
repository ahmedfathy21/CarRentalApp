using CarRentalApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentalApp.Infrastructure.Persistence.Configuration;

public class CustomerConfiguration :IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(c => c.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(c => c.NationalId)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.DriverLicense)
            .HasMaxLength(50);

        builder.Property(c => c.DateOfBirth)
            .IsRequired();

        builder.Property(c => c.Address)
            .HasMaxLength(250);

        builder.Property(c => c.IsBlackListed)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(c => c.UserId)
            .HasMaxLength(450);   // Identity user ID length

        // ── Indexes ───────────────────────────────────────
        builder.HasIndex(c => c.Email).IsUnique();
        builder.HasIndex(c => c.NationalId).IsUnique();
        builder.HasIndex(c => c.UserId);
    }
}