using CarRentalApp.Domain.Entities;
using CarRentalApp.Domain.Interfaces.Repositories;
using CarRentalApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApp.Infrastructure.Repositories;

public class BookingExtraRepository : BaseRepository<BookingExtra>, IBookingExtraRepository
{
    public BookingExtraRepository(AppDbContext db) : base(db) { }

    public async Task<IEnumerable<BookingExtra>> GetAvailableAsync()
        => await _dbSet
            .Where(e => e.IsAvailable)
            .AsNoTracking()
            .ToListAsync();

    public async Task<bool> NameExistsAsync(string name)
        => await _dbSet.AnyAsync(e => e.Name == name);
}