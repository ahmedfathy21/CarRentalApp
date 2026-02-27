using CarRentalApp.Domain.Entities;

namespace CarRentalApp.Domain.Interfaces.Repositories;

public interface ICustomerRepository :IBaseRepository<Customer>
{
    Task<Customer?> GetByEmailAsync (string email);
    Task<Customer?> GetByNationalIdAsync (string nationalId);
    Task<Customer?> GetByUserIdAsync (string userId);
    Task<Customer?> GetWithBookingAsync (int customerId);

    Task<IEnumerable<Customer>> GetBlackListedAsync();
    
    Task<bool> EmailExistsAsync(string email);
    Task<bool> NationalIdExistsAsync(string nationalId);


}