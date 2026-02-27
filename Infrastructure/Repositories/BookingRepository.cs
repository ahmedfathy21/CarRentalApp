using CarRentalApp.Domain.Entities;
using CarRentalApp.Domain.Enums;
using CarRentalApp.Domain.Interfaces.Repositories;
using CarRentalApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApp.Infrastructure.Repositories;

public class BookingRepository : BaseRepository<Booking>, IBookingRepository
{
    public BookingRepository(AppDbContext db) : base(db)
    {
    }

    public async  Task<Booking?> GetByBookingNumberAsync(string bookingNumber) =>
        await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.BookingNumber == bookingNumber);

    public async Task<Booking?> GetWithDetailsAsync(int bookingId) => await _dbSet
        .Include(x => x.Car)
        .ThenInclude(x => x.Category)
        .Include(x => x.Customer)
        .Include(x => x.ExtraLines)
        .ThenInclude(x => x.BookingExtra)
        .Include(x => x.Payment)
        .FirstOrDefaultAsync(x => x.Id == bookingId);



    public async Task<Booking?> GetWithExtrasAsync(int bookingId) => await _dbSet.Include(x => x.ExtraLines)
        .ThenInclude(x=> x.BookingExtra).FirstOrDefaultAsync(x => x.Id == bookingId);

    public async Task<Booking?> GetWithPaymentAsync(int bookingId) => await _dbSet.Include(x => x.Payment)
        .FirstOrDefaultAsync(x => x.Id == bookingId);


    public async Task<IEnumerable<Booking>> GetByCustomerAsync(int customerId) => await _dbSet
        .Where(x => x.CustomerId == customerId)
        .AsNoTracking()
        .OrderByDescending(b =>b.CreatedAt)
        .ToListAsync();

    public async Task<IEnumerable<Booking>> GetByCarAsync(int carId) => await _dbSet
        .Where(x => x.CarId == carId)
        .AsNoTracking()
        .OrderByDescending(b => b.CreatedAt)
        .ToListAsync();
    
    

    public async Task<IEnumerable<Booking>> GetByStatusAsync(BookingStatus status)
        => await _dbSet
            .Where(b => b.Status == status)
            .AsNoTracking()
            .ToListAsync();

    public async Task<IEnumerable<Booking>> GetActiveBookingsAsync()
        => await _dbSet
            .Where(b => b.Status == BookingStatus.Active)
            .Include(b => b.Car)
            .Include(b => b.Customer)
            .AsNoTracking()
            .ToListAsync();

    public async Task<IEnumerable<Booking>> GetUpcomingBookingsAsync()
        => await _dbSet
            .Where(b => b.Status == BookingStatus.Confirmed &&
                        b.StartDate > DateTime.UtcNow)
            .AsNoTracking()
            .OrderBy(b => b.StartDate)
            .ToListAsync();

    public async Task<IEnumerable<Booking>> GetOverdueBookingsAsync()
        => await _dbSet
            .Where(b => b.Status == BookingStatus.Active &&
                        b.EndDate < DateTime.UtcNow)
            .Include(b => b.Car)
            .Include(b => b.Customer)
            .AsNoTracking()
            .ToListAsync();

    public async Task<IEnumerable<Booking>> GetByDateRangeAsync(
        DateTime start, DateTime end)
        => await _dbSet
            .Where(b => b.StartDate >= start && b.EndDate <= end)
            .AsNoTracking()
            .ToListAsync();

    // ── Conflict Check ────────────────────────────────────────────

    public async Task<bool> HasOverlappingBookingAsync(
        int carId, DateTime startDate, DateTime endDate,
        int? excludeBookingId = null)
        => await _dbSet.AnyAsync(b =>
            b.CarId == carId &&
            b.Status != BookingStatus.Cancelled &&
            b.StartDate < endDate &&
            b.EndDate > startDate &&
            (excludeBookingId == null || b.Id != excludeBookingId));
}