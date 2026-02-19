namespace CarRentalApp.Domain.Entities;

public class BookingExtra :BaseEntity
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public decimal DailyPrice { get; private set; }
    public bool IsAvailable { get; private set; }
    
    // Navigation
    public ICollection<BookingExtra> BookingExtraLines { get; private set;  } = new List<BookingExtra>();
    
    private BookingExtra() { }

    public BookingExtra(string name, string? description, decimal dailyPrice)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be null or empty");
        if (dailyPrice <= 0) throw new ArgumentException("Daily price must be greater than zero");
        
        Name = name;
        Description = description;
        DailyPrice = dailyPrice;
        IsAvailable =  true;
    }
    
    public void Disable() => IsAvailable = false;
    public void Enable() =>  IsAvailable = true;

    public void UpdatePrice(decimal newDailyPrice)
    {
        if (newDailyPrice < 0)  throw new ArgumentException("Daily price must be greater than zero");
        DailyPrice = newDailyPrice;
        SetUpdatedAt();
    }

    /// <summary>
    /// Junction entity — which extras are attached to a specific booking, and the locked-in price.
    /// <summary/>

    public class BookingExtraLine
    {
        public int BookingId { get; private set; }
        public Booking Booking { get; private set; } = null!;
        
        public int BookingExtraId { get; private set; }
        public BookingExtra BookingExtra { get; private set; } = null!;
        
        public decimal PriceAtBooking { get; private set; }
        public int Quantity { get; private set; }
     
        private BookingExtraLine() { }

        public BookingExtraLine(int bookingId, int bookingExtraId, decimal priceAtBooking, int quantity = 1)
        {
            if (quantity < 1)           throw new ArgumentException("Quantity must be at least 1.");
            if (priceAtBooking < 0)     throw new ArgumentException("Price cannot be negative.");

            BookingId       = bookingId;
            BookingExtraId  = bookingExtraId;
            PriceAtBooking  = priceAtBooking;
            Quantity        = quantity;
        }

        public decimal TotalPrice => PriceAtBooking * Quantity;
    }
}