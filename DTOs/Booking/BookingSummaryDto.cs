namespace CarRentalApp.DTOs.Booking;

public class BookingSummaryDto
{
    public int      Id            { get; set; }
    public string   BookingNumber { get; set; } = string.Empty;
    public string   CustomerName  { get; set; } = string.Empty;
    public string   CarName       { get; set; } = string.Empty;
    public DateTime StartDate     { get; set; }
    public DateTime EndDate       { get; set; }
    public decimal  GrandTotal    { get; set; }
    public string   Status        { get; set; } = string.Empty;
}