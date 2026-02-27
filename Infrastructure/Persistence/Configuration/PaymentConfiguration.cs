using CarRentalApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentalApp.Infrastructure.Persistence.Configuration;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Amount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(p => p.Method)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(p => p.TransactionId)
            .HasMaxLength(100);

        builder.Property(p => p.PaidAt);

        builder.Property(p => p.Notes)
            .HasMaxLength(500);

        // ── Indexes ───────────────────────────────────────
        builder.HasIndex(p => p.BookingId).IsUnique(); // one payment per booking
        builder.HasIndex(p => p.TransactionId);

        // ── Relationships ─────────────────────────────────
        builder.HasOne(p => p.Booking)
            .WithOne(b => b.Payment)        // one-to-one
            .HasForeignKey<Payment>(p => p.BookingId)
            .OnDelete(DeleteBehavior.Cascade); // delete booking = delete payment
    }
}