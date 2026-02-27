using CarRentalApp.Domain.Entities;
using CarRentalApp.Domain.Enums;
using CarRentalApp.Domain.Interfaces.Repositories;
using CarRentalApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApp.Infrastructure.Repositories;

public class CarRepository :BaseRepository<Car> ,ICarRepository
{
    public CarRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Car?> GetByLicesnsePlateAsync(string licensePlate) => await
        _dbSet.AsNoTracking()
            .FirstOrDefaultAsync(x => x.LicensePlate == licensePlate);
    

    public async Task<Car?> GetWithCategoryAsync(int CarId) => await 
    _dbSet.AsNoTracking()
        .Include(x => x.Category)
    .FirstOrDefaultAsync(x => x.Id == CarId);
    

    public async Task<Car?> GetWithBookingAsync(int branchId) => await _dbSet.AsNoTracking()
        .Include(x=>x.Bookings)
    .FirstOrDefaultAsync(x => x.BranchId == branchId);

  /// <summary>
  /// Collection query
  /// </summary>
  /// <param name="branchId"></param>
  /// <returns>The collection of cars at a certain branch </returns>
    public async Task<IEnumerable<Car>> GetByBranchAsync(int branchId) => await _dbSet.AsNoTracking()
        .Where(x => x.BranchId == branchId)
        .ToListAsync();

/// <summary>
/// Collection query
/// </summary>
/// <param name="categoryId"></param>
/// <returns>The collection of cars at a certain Category  </returns>
    public async Task<IEnumerable<Car>> GetByCategoryAsync(int categoryId) => await _dbSet.AsNoTracking()
        .Where(x => x.CategoryId == categoryId)
        .ToListAsync();

/// <summary>
/// Collection query
/// </summary>
/// <param name="status"></param>
/// <returns>The collection of cars at a certain Status</returns>
    public async Task<IEnumerable<Car>> GetByStatusAsync(CarStatus status) => await _dbSet.AsNoTracking()
        .Where(x => x.Status == status)
        .ToListAsync();


    public async Task<IEnumerable<Car>> GetAvailableAsync(DateTime startDate, DateTime endDate) => await
        _dbSet.AsNoTracking()
            .Where(x => x.Status == CarStatus.Available && x.Bookings.Any(b => b.Status != BookingStatus.Cancelled
         && b.StartDate < endDate && b.EndDate > startDate))
            .ToListAsync();

    public async Task<IEnumerable<Car>> GetAvailableByBranchAsync(int branchId, DateTime startDate, DateTime endDate) =>
        await
            _dbSet.AsNoTracking()
                .Where(x => x.BranchId == branchId && x.Status == CarStatus.Available &&
                            x.Bookings.Any(b => b.Status != BookingStatus.Cancelled && 
                                                b.StartDate < endDate && b.EndDate > startDate))
                .ToListAsync();
   

    public async Task<IEnumerable<Car>> GetAvailableByCategoryAsync(int categoryId, DateTime startDate, DateTime endDate)=>
        await
            _dbSet.AsNoTracking()
                .Where(x => x.BranchId == categoryId && x.Status == CarStatus.Available &&
                            x.Bookings.Any(b => b.Status != BookingStatus.Cancelled && 
                                                b.StartDate < endDate && b.EndDate > startDate))
                .ToListAsync();
    
}