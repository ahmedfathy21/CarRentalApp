namespace CarRentalApp.Domain.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    // ── All Repos accessible through UoW ──────────────
    ICarRepository          Cars          { get; }
    IBookingRepository      Bookings      { get; }
    ICustomerRepository     Customers     { get; }
    IPaymentRepository      Payments      { get; }
    IBranchRepository       Branches      { get; }
    ICategoryRepository     Categories    { get; }
    IBookingExtraRepository BookingExtras { get; }

    // ── Transaction Control ───────────────────────────
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}