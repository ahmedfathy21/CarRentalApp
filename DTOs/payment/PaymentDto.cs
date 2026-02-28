namespace CarRentalApp.DTOs.payment;

public class PaymentDto
{
    public int      Id            { get; set; }
    public decimal  Amount        { get; set; }
    public string   Status        { get; set; } = string.Empty;
    public string   Method        { get; set; } = string.Empty;
    public string?  TransactionId { get; set; }
    public DateTime? PaidAt       { get; set; }
    public string   BookingNumber { get; set; } = string.Empty;
}