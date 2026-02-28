namespace CarRentalApp.DTOs.Booking;

public class CreatingBookingDto
{
    public int           CustomerId     { get; set; }
    public int           CarId          { get; set; }
    public DateTime      StartDate      { get; set; }
    public DateTime      EndDate        { get; set; }
    public string        PaymentMethod  { get; set; } = string.Empty;
    public List<int>     ExtraIds       { get; set; } = new();  // selected extra IDs
    public string?       Notes          { get; set; }
}       