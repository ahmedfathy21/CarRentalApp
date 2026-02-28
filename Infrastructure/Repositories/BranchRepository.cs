using CarRentalApp.Domain.Entities;
using CarRentalApp.Domain.Interfaces.Repositories;
using CarRentalApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApp.Infrastructure.Repositories;

public class BranchRepository :BaseRepository<Branch> , IBranchRepository
{
    public BranchRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Branch?> GetWithCarsAsync(int branchId) => await 
    _dbSet.AsNoTracking()
        .Include(x => x.Cars)
    .FirstOrDefaultAsync(x => x.Id == branchId);
    

    public async Task<IEnumerable<Branch>> GetActiveAsync() => await _dbSet.AsNoTracking()
    .Where(x => x.IsActive == true)
    .ToListAsync();
    

    public async Task<IEnumerable<Branch>> GetByCityAsync(string city) => await _dbSet.AsNoTracking()
        .Where(x => x.City == city)
    .ToListAsync();
    

    public async Task<bool> NameExistAsync(string name) => await 
        _dbSet.AnyAsync(x => x.Name == name);
    
}