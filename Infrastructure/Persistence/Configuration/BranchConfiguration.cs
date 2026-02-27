using CarRentalApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentalApp.Infrastructure.Persistence.Configuration;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        // This is the table 
        builder.ToTable("Branches");
        // This is the primary key
        builder.HasKey(b => b.Id);
        // This is the name property
        builder.Property(b => b.Name).IsRequired()
            .HasMaxLength(256);
        // This is the address property
        builder.Property(b => b.Address).IsRequired()
            .HasMaxLength(256);
        // This is the phone number property
        builder.Property(b => b.PhoneNumber).IsRequired()
            .HasMaxLength(20);
        // This is the city property
        builder.Property(b => b.City).IsRequired()
            .HasMaxLength(100);
        // This is the is active property
        builder.Property(b=>b.IsActive).IsRequired()
            .HasDefaultValue(true);
        
        // This is the unique index
        builder.HasIndex(b => b.Name).IsUnique();
        
    }
}