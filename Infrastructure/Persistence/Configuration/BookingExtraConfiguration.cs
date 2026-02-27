using CarRentalApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentalApp.Infrastructure.Persistence.Configuration;

public class BookingExtraConfiguration :IEntityTypeConfiguration<BookingExtra>
{
    public void Configure(EntityTypeBuilder<BookingExtra> builder)
    {
        builder.ToTable("BookingExtras");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Description)
            .HasMaxLength(500);

        builder.Property(e => e.DailyPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(e => e.IsAvailable)
            .IsRequired()
            .HasDefaultValue(true);

        // ── Indexes ───────────────────────────────────────
        builder.HasIndex(e => e.Name).IsUnique();
    }
}