namespace CarRentalApp.DTOs.payment;

public class ProcessPaymentDto
{
    public int    BookingId    { get; set; }
    public string Method       { get; set; } = string.Empty;
    public string? PaymentToken{ get; set; }  // from Stripe frontend
}