using CarRentalApp.Domain.Enums;
using CarRentalApp.Domain.Exceptions;

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
    public ICollection<BookingExtraLine> ExtraLines { get; private set; } = (ICollection<BookingExtraLine>)new List<BookingExtraLine>();
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
            throw new InvalidBookingStatusTransitionException(Status.ToString(), BookingStatus.Confirmed.ToString());
        Status = BookingStatus.Confirmed;
        SetUpdatedAt();
    }
    public void Cancel(string? reason = null)
    {
        if (Status == BookingStatus.Completed)
            throw new BookingNotCancellableException(Id);
        if (Status == BookingStatus.Cancelled)
            throw new BookingNotCancellableException(Id);

        Status = BookingStatus.Cancelled;
        Notes  = reason;
        SetUpdatedAt();
    }
    public void MarkAsActive()
    {
        if (Status != BookingStatus.Confirmed)
            throw new InvalidBookingStatusTransitionException(Status.ToString(), BookingStatus.Active.ToString());
        Status = BookingStatus.Active;
        SetUpdatedAt();
    }
    public void Complete(DateTime returnDate)
    {
        if (Status != BookingStatus.Active)
            throw new InvalidBookingStatusTransitionException(Status.ToString(), BookingStatus.Completed.ToString());

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
    public void AddExtra(BookingExtraLine extraLine)
    {
        if (Status != BookingStatus.Pending && Status != BookingStatus.Confirmed)
            throw new BookingModificationNotAllowedException(Id, "add extras", Status.ToString());

        ExtraLines.Add(extraLine);
        SetUpdatedAt();
    }
    public void Extend(DateTime newEndDate)
    {
        if (Status != BookingStatus.Active)
            throw new BookingModificationNotAllowedException(Id, "be extended", Status.ToString());
        if (newEndDate <= EndDate)
            throw new InvalidBookingPeriodException("new end date must be after current end date.");

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
            throw new InvalidBookingPeriodException("start date cannot be in the past.");
        if (end.Date <= start.Date)
            throw new InvalidBookingPeriodException("end date must be after start date.");
        if ((end - start).Days > 90)
            throw new InvalidBookingPeriodException("booking cannot exceed 90 days.");
    }

    private static string GenerateBookingNumber() =>
        $"BK-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..4].ToUpper()}";
}
