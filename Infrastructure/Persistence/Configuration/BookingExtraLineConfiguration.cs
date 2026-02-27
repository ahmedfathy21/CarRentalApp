using CarRentalApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentalApp.Infrastructure.Persistence.Configuration;

public class BookingExtraLineConfiguration : IEntityTypeConfiguration<BookingExtraLine>
{
    public void Configure(EntityTypeBuilder<BookingExtraLine> builder)
    {
        builder.ToTable("BookingExtraLines");

        // ── Composite Primary Key ─────────────────────────
        // No Id here — the combination of both FKs is the PK
        builder.HasKey(el => new { el.BookingId, el.BookingExtraId });

        builder.Property(el => el.PriceAtBooking)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(el => el.Quantity)
            .IsRequired()
            .HasDefaultValue(1);

        // ── Relationships ─────────────────────────────────
        builder.HasOne(el => el.Booking)
            .WithMany(b => b.ExtraLines)
            .HasForeignKey(el => el.BookingId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(el => el.BookingExtra)
            .WithMany(e => e.BookingExtraLines)
            .HasForeignKey(el => el.BookingExtraId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}