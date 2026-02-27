using CarRentalApp.Domain.Entities;
using CarRentalApp.Domain.Enums;
using CarRentalApp.Domain.Interfaces.Repositories;
using CarRentalApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApp.Infrastructure.Repositories;

public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
{
    public PaymentRepository(AppDbContext db) : base(db) { }

    public async Task<Payment?> GetByBookingIdAsync(int bookingId)
        => await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.BookingId == bookingId);

    public async Task<Payment?> GetByTransactionIdAsync(string transactionId)
        => await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.TransactionId == transactionId);

    public async Task<IEnumerable<Payment>> GetByStatusAsync(PaymentStatus status)
        => await _dbSet
            .Where(p => p.Status == status)
            .AsNoTracking()
            .ToListAsync();

    public async Task<IEnumerable<Payment>> GetByDateRangeAsync(
        DateTime start, DateTime end)
        => await _dbSet
            .Where(p => p.PaidAt >= start && p.PaidAt <= end)
            .AsNoTracking()
            .ToListAsync();

    public async Task<decimal> GetTotalRevenueAsync(DateTime start, DateTime end)
        => await _dbSet
            .Where(p => p.Status == PaymentStatus.Paid &&
                        p.PaidAt >= start &&
                        p.PaidAt <= end)
            .SumAsync(p => p.Amount);

    
}