namespace CarRentalApp.Domain.Exceptions;

public class PaymentNotFoundException : DomainException
{
    public PaymentNotFoundException(int paymentId)
        : base("PAYMENT_NOT_FOUND", $"Payment with ID {paymentId} was not found.") { }
}

public class PaymentAlreadyProcessedException : DomainException
{
    public PaymentAlreadyProcessedException(int paymentId)
        : base("PAYMENT_ALREADY_PROCESSED", $"Payment with ID {paymentId} has already been processed.") { }
}

public class PaymentFailedException : DomainException
{
    public PaymentFailedException(string reason)
        : base("PAYMENT_FAILED", $"Payment processing failed: {reason}") { }
}

public class RefundNotAllowedException : DomainException
{
    public RefundNotAllowedException(int paymentId)
        : base("REFUND_NOT_ALLOWED", $"Payment with ID {paymentId} is not eligible for a refund.") { }
}

public class InvalidPaymentAmountException : DomainException
{
    public InvalidPaymentAmountException(decimal amount)
        : base("INVALID_PAYMENT_AMOUNT",
            $"Payment amount must be greater than zero. Received: {amount}.") { }
}
