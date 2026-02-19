namespace CarRentalApp.Domain.Enums;

public enum PaymentStatus
{
    Pending  = 1,   // Payment record created, waiting to be processed
    Paid     = 2,   // Payment was successfully charged
    Refunded = 3,   // Money was returned to the customer (e.g. after cancellation)
    Failed   = 4    // Payment attempt failed (card declined, timeout...)
}