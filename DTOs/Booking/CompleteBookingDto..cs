namespace CarRentalApp.DTOs.Booking;

public class CompleteBookingDto_
{
    public int      BookingId       { get; set; }
    public DateTime ReturnDate      { get; set; }
    public int      FinalMileage    { get; set; }
    public string?  Notes           { get; set; }
}