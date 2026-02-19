using CarRentalApp.Domain.Enums;

namespace CarRentalApp.Domain.Entities;

public class Booking : BaseEntity
{
    public string BookingNumber { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public DateTime? ActualReturnDate { get; private set; }
    public BookingStatus Status { get; private set; }
    
    public decimal CarDailyRate { get; private set; }
    public decimal TotalCost { get; private set; }
    public Decimal? LateFee { get; private set; }
    public string? Notes { get; private set; }
    
    // Fk 
    public int CustomerId { get; private set; }
    public Customer Customer { get; private set; } = null!;
    public int CarId { get; private set; }
    public Car Car { get; private set; } = null!;
    
    // Navigation
    public ICollection<BookingExtra.BookingExtraLine> ExtraLines { get; private set; } = new List<BookingExtra.BookingExtraLine>();
    public Payment? Payment { get; private set; }
    
    private Booking(){}
    public Booking(int customerId, int carId, DateTime startDate,
        DateTime endDate, decimal carDailyRate)
    {
        ValidateDates(startDate, endDate);

        CustomerId    = customerId;
        CarId         = carId;
        StartDate     = startDate;
        EndDate       = endDate;
        CarDailyRate  = carDailyRate;
        Status        = BookingStatus.Pending;
        BookingNumber = GenerateBookingNumber();
        TotalCost   = CalculateBaseAmount();
    }
    // Computed
    public int RentalDays => (EndDate - StartDate).Days;

    public decimal ExtrasTotal => ExtraLines.Sum(e => e.PriceAtBooking * e.Quantity * e.RentalDays);

    public decimal GrandTotal => TotalCost + ExtrasTotal + (LateFee ?? 0);

    // Business Methods 

    public void Confirm()
    {
        if (Status != BookingStatus.Pending)
            throw new ArgumentException("Only Pending bookings can be confirmed!");
        Status = BookingStatus.Confirmed;
        SetUpdatedAt();
    }
    public void Cancel(string? reason = null)
    {
        if (Status == BookingStatus.Completed)
            throw new InvalidOperationException("Cannot cancel a completed booking.");
        if (Status == BookingStatus.Cancelled)
            throw new InvalidOperationException("Booking is already cancelled.");

        Status = BookingStatus.Cancelled;
        Notes  = reason;
        SetUpdatedAt();
    }
    public void MarkAsActive()
    {
        if (Status != BookingStatus.Confirmed)
            throw new InvalidOperationException("Only confirmed bookings can be activated.");
        Status = BookingStatus.Active;
        SetUpdatedAt();
    }
    public void Complete(DateTime returnDate)
    {
        if (Status != BookingStatus.Active)
            throw new InvalidOperationException("Only active bookings can be completed.");

        ActualReturnDate = returnDate;

        if (returnDate.Date > EndDate.Date)
        {
            int extraDays = (returnDate.Date - EndDate.Date).Days;
            LateFee = extraDays * CarDailyRate * 1.5m;  // 50% surcharge for late return
        }

        Status = BookingStatus.Completed;
        RecalculateTotalAmount();
        SetUpdatedAt();
    }
    public void AddExtra(BookingExtra.BookingExtraLine extraLine)
    {
        if (Status != BookingStatus.Pending && Status != BookingStatus.Confirmed)
            throw new InvalidOperationException("Cannot add extras to an active or completed booking.");

        ExtraLines.Add(extraLine);
        SetUpdatedAt();
    }
    public void Extend(DateTime newEndDate)
    {
        if (Status != BookingStatus.Active)
            throw new InvalidOperationException("Only active bookings can be extended.");
        if (newEndDate <= EndDate)
            throw new ArgumentException("New end date must be after current end date.");

        EndDate     = newEndDate;
        TotalCost = CalculateBaseAmount();
        SetUpdatedAt();
    }
    //  Private Helpers 

    private decimal CalculateBaseAmount() => RentalDays * CarDailyRate;

    private void RecalculateTotalAmount()
    {
        int actualDays = ActualReturnDate.HasValue
            ? Math.Max((ActualReturnDate.Value.Date - StartDate.Date).Days, 1)
            : RentalDays;
        TotalCost = actualDays * CarDailyRate;
    }

    private static void ValidateDates(DateTime start, DateTime end)
    {
        if (start.Date < DateTime.UtcNow.Date)
            throw new ArgumentException("Start date cannot be in the past.");
        if (end.Date <= start.Date)
            throw new ArgumentException("End date must be after start date.");
        if ((end - start).Days > 90)
            throw new ArgumentException("Booking cannot exceed 90 days.");
    }

    private static string GenerateBookingNumber() =>
        $"BK-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..4].ToUpper()}";
}