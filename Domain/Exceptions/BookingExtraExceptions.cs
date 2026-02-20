namespace CarRentalApp.Domain.Exceptions;

public class InvalidBookingExtraDataException : DomainException
{
    public InvalidBookingExtraDataException(string reason)
        : base("INVALID_BOOKING_EXTRA_DATA", $"Invalid booking extra data: {reason}") { }
}
