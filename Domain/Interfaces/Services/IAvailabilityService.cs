using CarRentalApp.Domain.Entities;

namespace CarRentalApp.Domain.Interfaces.Services;

public interface IAvailabilityService
{
    /// <summary>Returns true if the car has no conflicting bookings in the period.</summary>
    Task<bool> IsCarAvailableAsync(int carId, DateTime startDate, DateTime endDate);

    /// <summary>Returns all available cars matching the filters.</summary>
    Task<IEnumerable<Car>> GetAvailableCarsAsync(DateTime startDate, DateTime endDate,
        int? categoryId = null,
        int? branchId   = null);
}