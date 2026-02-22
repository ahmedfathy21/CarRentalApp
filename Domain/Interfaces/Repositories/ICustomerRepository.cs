using CarRentalApp.Domain.Entities;

namespace CarRentalApp.Domain.Interfaces.Repositories;

public interface ICustomerRepository :IBaseRepository<Customer>
{
    Task<Customer> GetByEmailAsync (string email);
    Task<Customer> GetByNationalIdAsync (string NationalId);
    Task<Customer> GetByUserIdAsync (string UserId);
    Task<Customer> GetWithBookingAsync (int CustomerId);

    Task<IEnumerable<Customer>> GetBlackListedAsync();
    
    Task<bool> EmailExistsAsync(string email);
    Task<bool> NationalIdExistsAsync(string nationalId);


}