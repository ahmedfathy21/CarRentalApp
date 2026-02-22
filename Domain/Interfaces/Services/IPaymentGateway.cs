namespace CarRentalApp.Domain.Interfaces.Services;

public interface IPaymentGateway
{
    /// <summary>Charges the customer and returns a transaction ID.</summary>
    Task<PaymentResult> ChargeAsync(decimal amount, string currency,
        string paymentToken, string description);

    /// <summary>Refunds a previously charged transaction.</summary>
    Task<PaymentResult> RefundAsync(string transactionId, decimal amount);
}

public class PaymentResult
{
    public bool   IsSuccess     { get; init; }
    public string TransactionId { get; init; } = string.Empty;
    public string? ErrorMessage { get; init; }
}
