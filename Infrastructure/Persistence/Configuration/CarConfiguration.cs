using CarRentalApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentalApp.Infrastructure.Persistence.Configuration;

public class CarConfiguration :IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.ToTable("Cars");
        
        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.Make).HasMaxLength(50).IsRequired();
        builder.Property(b => b.Description).HasMaxLength(256).IsRequired();
        builder.Property(b => b.Model).HasMaxLength(50).IsRequired();
        builder.Property(b => b.Color).HasMaxLength(30).IsRequired();
        builder.Property(b => b.LicensePlate).HasMaxLength(20).IsRequired();
        builder.Property(b => b.ImageUrl).HasMaxLength(512).IsRequired();
        builder.Property(b => b.DailyRate).HasColumnType("decimal(18,2)");
        builder.Property(b => b.Status).IsRequired().HasConversion<string>().HasMaxLength(30);
        builder.Property(b => b.Mileage).IsRequired().HasMaxLength(256).HasDefaultValue(0);

        
        
        builder.HasOne(b => b.Category).WithMany(c => c.Cars).HasForeignKey(b => b.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(b => b.Branch).WithMany(b => b.Cars).HasForeignKey(b => b.BranchId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(b => new { b.LicensePlate}).IsUnique();
        builder.HasIndex(b => new { b.CategoryId});
        builder.HasIndex(b => new { b.Status});
        builder.HasIndex(b => new { b.CategoryId ,b.Status});

    }
}