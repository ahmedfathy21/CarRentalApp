using CarRentalApp.Domain.Entities;

namespace CarRentalApp.Domain.Interfaces.Repositories;

public interface IBookingExtraRepository : IBaseRepository<BookingExtra>
{
    Task<IEnumerable<BookingExtra>> GetAvailableAsync();
    Task<bool> NameExistsAsync(string name);
}

    
