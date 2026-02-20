namespace CarRentalApp.Domain.Exceptions;

public class BookingNotFoundException : DomainException
{
    public BookingNotFoundException(int bookingId)
        : base("BOOKING_NOT_FOUND", $"Booking with ID {bookingId} was not found.") { }
}

public class InvalidBookingPeriodException : DomainException
{
    public InvalidBookingPeriodException(string reason)
        : base("INVALID_BOOKING_PERIOD", $"Invalid booking period: {reason}") { }
}

public class BookingNotCancellableException : DomainException
{
    public BookingNotCancellableException(int bookingId)
        : base("BOOKING_NOT_CANCELLABLE", $"Booking {bookingId} cannot be cancelled in its current status.") { }
}

public class InvalidBookingStatusTransitionException : DomainException
{
    public InvalidBookingStatusTransitionException(string from, string to)
        : base("INVALID_STATUS_TRANSITION", $"Cannot transition booking from '{from}' to '{to}'.") { }
}

public class BookingModificationNotAllowedException : DomainException
{
    public BookingModificationNotAllowedException(int bookingId, string action, string currentStatus)
        : base("BOOKING_MODIFICATION_NOT_ALLOWED",
            $"Booking {bookingId} cannot {action} while status is '{currentStatus}'.") { }
}
