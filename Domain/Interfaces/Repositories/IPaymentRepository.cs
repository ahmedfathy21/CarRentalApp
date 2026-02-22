using CarRentalApp.Domain.Entities;
using CarRentalApp.Domain.Enums;

namespace CarRentalApp.Domain.Interfaces.Repositories;

public interface IPaymentRepository :IBaseRepository<Payment>
{
    Task<Payment?> GetByBookingAsync(int bookingId);
    Task<Payment?> GetByTransactionIdAsync(string transactionId);
    
    Task<IEnumerable<Payment>> GetByStatusAsync(PaymentStatus status);
    Task<IEnumerable<Payment>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
 
    Task<decimal> GetTotalRevnueAsync(DateTime startDate,DateTime endDate);
    
}