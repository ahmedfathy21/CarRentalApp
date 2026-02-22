using System.Linq.Expressions;
using CarRentalApp.Domain.Entities;
namespace CarRentalApp.Domain.Interfaces.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
    //Quires 
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);

    // ── Commands ──────────────────────────────────────
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    void Update(T entity);
    void Delete(T entity);        // Hard delete
    void SoftDelete(T entity);    // Sets IsDeleted = true
    
}

