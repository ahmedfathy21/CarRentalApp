namespace CarRentalApp.Domain.Enums;

public enum BookingStatus
{
    Pending   = 1,   // Customer submitted the booking, not yet confirmed
    Confirmed = 2,   // Admin/system confirmed it, payment might be pending
    Active    = 3,   // Customer has picked up the car, rental is ongoing
    Completed = 4,   // Car returned, booking closed
    Cancelled = 5    // Booking was cancelled by customer or admin
}