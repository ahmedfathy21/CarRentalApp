using CarRentalApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace CarRentalApp.Infrastructure.Persistence;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // ── DbSets — one per entity = one table in DB ─────────────────
    public DbSet<Branch>           Branches        { get; set; }
    public DbSet<Category>         Categories      { get; set; }
    public DbSet<Car>              Cars            { get; set; }
    public DbSet<Customer>         Customers       { get; set; }
    public DbSet<Booking>          Bookings        { get; set; }
    public DbSet<Payment>          Payments        { get; set; }
    public DbSet<BookingExtra>     BookingExtras   { get; set; }
    public DbSet<BookingExtraLine> BookingExtraLines { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Required for Identity tables
        
        //Apply all configurations from the configurations folder
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        
        //Global query filters 
        // Global Query filters
        modelBuilder.Entity<Branch>()         .HasQueryFilter(e=>!e.IsDeleted);
        modelBuilder.Entity<Payment>()        .HasQueryFilter(e=>!e.IsDeleted);
        modelBuilder.Entity<Customer>()       .HasQueryFilter(e=>!e.IsDeleted);
        modelBuilder.Entity<Car>()            .HasQueryFilter(e=>!e.IsDeleted);
        modelBuilder.Entity<Category>()       .HasQueryFilter(e=>!e.IsDeleted);
        modelBuilder.Entity<Booking>()        .HasQueryFilter(e=>!e.IsDeleted);
        modelBuilder.Entity<BookingExtra>()   .HasQueryFilter(e=>!e.IsDeleted);
        
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        SetAuditFields();
        return await base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        SetAuditFields();
        return base.SaveChanges();

    }

    private void SetAuditFields()
    {
        var entries = ChangeTracker.Entries<BaseEntity>();

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    // CreatedAt is already set in BaseEntity constructor
                    // but we set it here as a safety net
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;

                case EntityState.Modified:
                    entry.Entity.SetUpdatedAt();
                    // Prevent CreatedAt from being modified
                    entry.Property(e => e.CreatedAt).IsModified = false;
                    break;
            }
        }
    }


}

