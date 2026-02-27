using CarRentalApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentalApp.Infrastructure.Persistence.Configuration;

public class BookingConfiguration :IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Bookings");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.BookingNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(b => b.StartDate)
            .IsRequired();

        builder.Property(b => b.EndDate)
            .IsRequired();

        builder.Property(b => b.ActualReturnDate);

        builder.Property(b => b.CarDailyRate)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(b => b.TotalCost)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(b => b.LateFee)
            .HasColumnType("decimal(18,2)");

        builder.Property(b => b.Notes)
            .HasMaxLength(500);

        // ── Enum stored as string ─────────────────────────
        builder.Property(b => b.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(30);

        // ── Indexes ───────────────────────────────────────
        builder.HasIndex(b => b.BookingNumber).IsUnique();
        builder.HasIndex(b => b.Status);
        builder.HasIndex(b => b.StartDate);
        builder.HasIndex(b => b.CarId);
        builder.HasIndex(b => b.CustomerId);

        // ── Relationships ─────────────────────────────────
        builder.HasOne(b => b.Car)
            .WithMany(c => c.Bookings)
            .HasForeignKey(b => b.CarId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Customer)
            .WithMany(c => c.Bookings)
            .HasForeignKey(b => b.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}