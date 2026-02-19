using CarRentalApp.Domain.Enums;

namespace CarRentalApp.Domain.Entities;

public class Car :BaseEntity
{
    public string Make { get; private set; }
    public string Model { get; private set; }
    public string Color { get; private set; }
    public int Year { get; private set; }
    public string LicensePlate { get; private set; }
    public string Description { get; private set; }
    public string? ImageUrl { get; private set; }
    public decimal DailyRate{get ; private set;}
    public CarStatus Status { get; private set; }
    public int Mileage { get; private set; }
    
    //FK
    public int CategoryId { get; private set; }
    public Category? Category { get; private set; } = null!;
    public int BranchId { get; private set; }
    public Branch? Branch { get; private set; } = null!;
    
    //Navigation
    public ICollection<Booking> Bookings { get; private set; } = new List<Booking>();
    
    private Car(){ }
    public Car(string make, string model, int year, string licensePlate,
        string color, decimal dailyRate, int categoryId, int branchId)
    {
        Validate(make, model, year, licensePlate, dailyRate);

        Make         = make;
        Model        = model;
        Year         = year;
        LicensePlate = licensePlate;
        Color        = color;
        DailyRate    = dailyRate;
        CategoryId   = categoryId;
        BranchId     = branchId;
        Status       = CarStatus.Available;
        Mileage      = 0;
    }

    public void MarkAsRented()
    {
        if (Status != CarStatus.Available)
            throw new ArgumentException($"car is not available. Current status is {Status}");
    }
    public void MarkAsAvailable()
    {
        Status = CarStatus.Available;
        SetUpdatedAt();
    }

    public void SendToMaintenance()
    {
        Status = CarStatus.UnderMaintenance;
        SetUpdatedAt();
    }

    public void UpdateMileage(int newMileage)
    {
        if(newMileage < Mileage)
            throw new ArgumentException("new mileage cannot be less that the current mileage is ");
        Mileage = newMileage;
        SetUpdatedAt();
    }
    public void UpdateDailyRate(decimal newRate)
    {
        if (newRate <= 0)
            throw new ArgumentException("Daily rate must be greater than zero.");
        DailyRate = newRate;
        SetUpdatedAt();
    }
    public void SetImage(string imageUrl) { ImageUrl = imageUrl;  SetUpdatedAt(); }
    
    public bool IsAvailableForPeriod(DateTime start, DateTime end)
    {
        if (Status != CarStatus.Available) return false;

        return !Bookings.Any(b =>
            b.Status != BookingStatus.Cancelled &&
            b.StartDate < end && b.EndDate > start);
    }
    private static void Validate(string make, string model, int year,
        string licensePlate, decimal dailyRate)
    {
        if (string.IsNullOrWhiteSpace(make))        throw new ArgumentException("Make is required.");
        if (string.IsNullOrWhiteSpace(model))       throw new ArgumentException("Model is required.");
        if (year < 2000 || year > DateTime.UtcNow.Year + 1)
            throw new ArgumentException("Invalid year.");
        if (string.IsNullOrWhiteSpace(licensePlate))throw new ArgumentException("License plate is required.");
        if (dailyRate <= 0)                         throw new ArgumentException("Daily rate must be greater than zero.");
    }
}