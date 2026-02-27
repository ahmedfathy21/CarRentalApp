using CarRentalApp.Domain.Entities;
using CarRentalApp.Domain.Enums;
using CarRentalApp.Domain.Interfaces.Repositories;
using CarRentalApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace CarRentalApp.Infrastructure.Repositories;

public class CustomerRepository : BaseRepository<Customer> ,ICustomerRepository 
{
    public CustomerRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Customer?> GetByEmailAsync(string email) => await
        _dbSet.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email.ToLowerInvariant());
    

    public async Task<Customer?> GetByNationalIdAsync(string nationalId) => await 
    _dbSet.AsNoTracking()
        .FirstOrDefaultAsync(x => x.NationalId == nationalId);
    

    public async Task<Customer?> GetByUserIdAsync(string userId) => await 
    _dbSet.AsNoTracking()
    .FirstOrDefaultAsync(x => x.UserId == userId);


    public async Task<Customer?> GetWithBookingAsync(int customerId) => await
        _dbSet.AsNoTracking()
            .Include(x => x.Bookings)
            .ThenInclude(x => x.Car)
            .FirstOrDefaultAsync(x => x.Id == customerId);
    

    public async Task<IEnumerable<Customer>> GetBlackListedAsync() => await 
    _dbSet.AsNoTracking()
        .Where(x=>x.IsBlackListed == true)
        .ToListAsync();
    

    public async Task<bool> EmailExistsAsync(string email) => await 
        _dbSet.AnyAsync(x => x.Email == email.ToLowerInvariant());
    

    public async Task<bool> NationalIdExistsAsync(string nationalId) => await 
        _dbSet.AnyAsync(x => x.NationalId == nationalId);
    
}