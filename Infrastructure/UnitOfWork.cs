using System.Data;
using CarRentalApp.Domain.Entities;
using CarRentalApp.Domain.Interfaces.Repositories;
using CarRentalApp.Infrastructure.Persistence;
using CarRentalApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualBasic;

namespace CarRentalApp.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _db;
    

    // ── Transaction holder ────────────────────────────────────────
    private IDbContextTransaction? _transaction;

    // ── Repository instances — lazy initialized ───────────────────
    private ICarRepository? _cars;
    private IBookingRepository? _bookings;
    private ICustomerRepository? _customers;
    private IPaymentRepository? _payments;
    private IBranchRepository? _branches;
    private ICategoryRepository? _categories;
    private IBookingExtraRepository? _bookingExtras;

    public UnitOfWork(AppDbContext db)
    {
        _db = db;
    }

    // ── Repository Properties — Lazy Initialization ───────────────
    // Each repo is only created the first time it is accessed
    // If a service never uses Payments — PaymentRepository is never created

    public ICarRepository Cars
        => _cars ??= new CarRepository(_db);

    public IBookingRepository Bookings
        => _bookings ??= new BookingRepository(_db);

    public ICustomerRepository Customers
        => _customers ??= new CustomerRepository(_db);

    public IPaymentRepository Payments
        => _payments ??= new PaymentRepository(_db);

    public IBranchRepository Branches
        => _branches ??= new BranchRepository(_db);

    public ICategoryRepository Categories
        => _categories ??= new CategoryRepository(_db);

    public IBookingExtraRepository BookingExtras
        => _bookingExtras ??= new BookingExtraRepository(_db);

    // ── Save ──────────────────────────────────────────────────────

    public async Task<int> SaveChangesAsync()
    {
        return await _db.SaveChangesAsync();
    }

    // ── Transaction Control ───────────────────────────────────────

    public async Task BeginTransactionAsync()
    {
        _transaction = await _db.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await _db.SaveChangesAsync();
            await _transaction!.CommitAsync();
        }
        finally
        {
            await _transaction!.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        try
        {
            await _transaction!.RollbackAsync();
        }
        finally
        {
            await _transaction!.DisposeAsync();
            _transaction = null;
        }
    }

    // ── Dispose ───────────────────────────────────────────────────

    public void Dispose()
    {
        _transaction?.Dispose();
        _db.Dispose();
    }
}