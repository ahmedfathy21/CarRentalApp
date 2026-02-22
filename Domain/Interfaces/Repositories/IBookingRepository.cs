using CarRentalApp.Domain.Entities;
using CarRentalApp.Domain.Enums;

namespace CarRentalApp.Domain.Interfaces.Repositories;

public interface IBookingRepository : IBaseRepository<Booking>
{
    // Single Booking Quires 
    Task<Booking?> GetByBookingNumberAsync(string bookingNumber);
    Task<Booking?> GetWithDetailsAsync(int bookingId);   // includes Car, Customer, Extras, Payment
    Task<Booking?> GetWithExtrasAsync(int bookingId);  
    Task<Booking?> GetWithPaymentAsync(int bookingId);
    
    
    // ── Collection Queries ────────────────────────────
    Task<IEnumerable<Booking>> GetByCustomerAsync(int customerId);
    Task<IEnumerable<Booking>> GetByCarAsync(int carId);
    Task<IEnumerable<Booking>> GetByStatusAsync(BookingStatus status);

    Task<IEnumerable<Booking>> GetActiveBookingsAsync();
    Task<IEnumerable<Booking>> GetUpcomingBookingsAsync();  // Confirmed, future StartDate

    /// <summary>
    /// Returns bookings where the car was not returned on time.
    /// </summary>
    Task<IEnumerable<Booking>> GetOverdueBookingsAsync();

    // ── Date Range Queries ────────────────────────────
    Task<IEnumerable<Booking>> GetByDateRangeAsync(DateTime start, DateTime end);

    // ── Conflict Check ────────────────────────────────
    /// <summary>
    /// Checks if a car already has an active/confirmed booking
    /// that overlaps with the requested period.
    /// </summary>
    Task<bool> HasOverlappingBookingAsync(int carId, DateTime startDate,
        DateTime endDate, int? excludeBookingId = null);

}