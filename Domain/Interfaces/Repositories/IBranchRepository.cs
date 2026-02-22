using CarRentalApp.Domain.Entities;

namespace CarRentalApp.Domain.Interfaces.Repositories;

public interface IBranchRepository :IBaseRepository<Branch>
{
    Task<Branch?> GetWithCarsAsync(int branchId);
    
    Task<IEnumerable<Branch>> GetActiveAsync();
    Task<IEnumerable<Branch>> GetByCityAsync(string city);
    Task<bool> NameExistAsync(string name);
    
}