namespace CarRentalApp.Domain.Enums;

public enum PaymentMethod
{
    Cash            = 1,   // Paid in person at the branch
    CreditCard      = 2,   // Charged via Stripe or similar
    DebitCard       = 3,
    OnlineTransfer  = 4    // Bank transfer
}