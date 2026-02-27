using CarRentalApp.Domain.Enums;
using CarRentalApp.Domain.Exceptions;

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
        if (Status == CarStatus.UnderMaintenance)
            throw new CarUnderMaintenanceException(Id);
        if (Status != CarStatus.Available)
            throw new CarNotAvailableException(Id);
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
            throw new InvalidMileageUpdateException(Mileage, newMileage);
        Mileage = newMileage;
        SetUpdatedAt();
    }
    public void UpdateDailyRate(decimal newRate)
    {
        if (newRate <= 0)
            throw new InvalidCarRateException(newRate);
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
        if (string.IsNullOrWhiteSpace(make))        throw new InvalidCarDataException("make is required.");
        if (string.IsNullOrWhiteSpace(model))       throw new InvalidCarDataException("model is required.");
        if (year < 2000 || year > DateTime.UtcNow.Year + 1)
            throw new InvalidCarDataException("year is out of valid range.");
        if (string.IsNullOrWhiteSpace(licensePlate))throw new InvalidCarDataException("license plate is required.");
        if (dailyRate <= 0)                         throw new InvalidCarDataException("daily rate must be greater than zero.");
    }
}
