using CarRentalApp.Domain.Enums;
namespace CarRentalApp.Domain.Entities;

    public class Payment : BaseEntity
    {
        public decimal Amount           { get; private set; }
        public PaymentStatus Status     { get; private set; }
        public PaymentMethod Method     { get; private set; }
        public string? TransactionId    { get; private set; }  // From Stripe or bank
        public DateTime? PaidAt         { get; private set; }
        public string? Notes            { get; private set; }

        // FK — one payment per booking
        public int BookingId            { get; private set; }
        public Booking Booking          { get; private set; } = null!;

        private Payment() { }

        public Payment(int bookingId, decimal amount, PaymentMethod method)
        {
            if (amount <= 0) throw new ArgumentException("Payment amount must be greater than zero.");

            BookingId = bookingId;
            Amount    = amount;
            Method    = method;
            Status    = PaymentStatus.Pending;
        }

        // ── Business Methods ──────────────────────────────────────────

        public void MarkAsPaid(string? transactionId = null)
        {
            if (Status == PaymentStatus.Paid)
                throw new InvalidOperationException("Payment is already marked as paid.");

            Status        = PaymentStatus.Paid;
            TransactionId = transactionId;
            PaidAt        = DateTime.UtcNow;
            SetUpdatedAt();
        }

        public void MarkAsFailed(string? reason = null)
        {
            Status = PaymentStatus.Failed;
            Notes  = reason;
            SetUpdatedAt();
        }

        public void Refund(string? reason = null)
        {
            if (Status != PaymentStatus.Paid)
                throw new InvalidOperationException("Only paid payments can be refunded.");

            Status = PaymentStatus.Refunded;
            Notes  = reason;
            SetUpdatedAt();
        }
    }
    
