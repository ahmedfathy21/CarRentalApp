using CarRentalApp.Domain.Entities;
using CarRentalApp.Domain.Enums;

namespace CarRentalApp.Domain.Interfaces.Repositories;

public interface ICarRepository : IBaseRepository<Car>
{
    // Quires 
    Task<Car?> GetByLicesnsePlateAsync (string licensePlate);
    Task<Car?> GetWithCategoryAsync (int CarId);
    Task<Car?> GetWithBookingAsync (int branchId);

    Task<IEnumerable<Car>> GetByBranchAsync(int branchId);
    Task<IEnumerable<Car>> GetByCategoryAsync(int categoryId);
    Task<IEnumerable<Car>> GetByStatusAsync(CarStatus status);
    
    /// <summary>
    /// Returns all cars with no overlapping confirmed/active bookings
    /// in the requested date range.
    /// </summary>
    
    Task<IEnumerable<Car>> GetAvailableAsync(DateTime startDate, DateTime endDate);

    Task<IEnumerable<Car>> GetAvailableByBranchAsync(int branchId,
        DateTime startDate,
        DateTime endDate);

    Task<IEnumerable<Car>> GetAvailableByCategoryAsync(int categoryId,
        DateTime startDate,
        DateTime endDate);
    
}