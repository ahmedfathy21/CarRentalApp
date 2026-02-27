using CarRentalApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentalApp.Infrastructure.Persistence.Configuration;

public class CategoryConfiguration :IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Name).IsRequired()
            .HasMaxLength(256);
        builder.Property(b => b.Description).IsRequired()
            .HasMaxLength(256);
        builder.Property(b => b.BaseDailyRate).IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.HasIndex(b => b.Name).IsUnique();
        
    }
}