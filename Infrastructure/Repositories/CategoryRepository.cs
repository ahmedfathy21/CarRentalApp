using CarRentalApp.Domain.Entities;
using CarRentalApp.Domain.Enums;
using CarRentalApp.Domain.Interfaces.Repositories;
using CarRentalApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApp.Infrastructure.Repositories;

public class CategoryRepository: BaseRepository<Category>,ICategoryRepository
{
    public CategoryRepository(AppDbContext db) : base(db) { }

    public async Task<Category?> GetWithCarsAsync(int categoryId)
        => await _dbSet
            .Include(c => c.Cars)
            .FirstOrDefaultAsync(c => c.Id == categoryId);

    public async Task<IEnumerable<Category>> GetWithAvailableCarsAsync()
        => await _dbSet
            .Include(c => c.Cars
                .Where(car => car.Status == CarStatus.Available))
            .AsNoTracking()
            .ToListAsync();

    public async Task<bool> NameExistsAsync(string name)
        => await _dbSet.AnyAsync(c => c.Name == name);

    
}