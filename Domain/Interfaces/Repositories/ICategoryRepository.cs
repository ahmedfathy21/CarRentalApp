using CarRentalApp.Domain.Entities;

namespace CarRentalApp.Domain.Interfaces.Repositories;

public interface ICategoryRepository :IBaseRepository<Category>
{
    Task<Category?> GetWithCarsAsync(int CategoryId);
    Task<IEnumerable<Category>> GetWithAvailableCarsAsync();
    Task<bool> NameExistsAsync(string name);
}