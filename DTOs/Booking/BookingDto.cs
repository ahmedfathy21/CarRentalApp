namespace CarRentalApp.DTOs.Booking;

public class BookingDto
{
    public int Id { get; set; }
    public string BookingNumber { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime? ActualReturnDate { get; set; }
    public int RentalDays { get; set; }
    public decimal TotalCost { get; set; }
    public decimal ExtrasTotal { get; set; }
    public decimal? LateFee           { get; set; }
    public decimal  GrandTotal        { get; set; }
    public string   Status            { get; set; } = string.Empty;
    public string?  Notes             { get; set; }
    
    
    // Flattened from Car
    public string   CarMake           { get; set; } = string.Empty;
    public string   CarModel          { get; set; } = string.Empty;
    public string   CarImageUrl       { get; set; } = string.Empty;
    public string   CategoryName      { get; set; } = string.Empty;
    
    // Flattened from Customer
    public string   CustomerFullName      { get; set; } = string.Empty;
    public string   CustomerEmailAddress  { get; set; } = string.Empty;
    public string   CustomerPhoneNumber   { get; set; } = string.Empty;
    
    // Flattened from payment 
    public string   PaymentStatus        { get; set; } = string.Empty;
    public string?  PaymentMethod        { get; set; }

    public List<BookingExtraDto> Extras { get; set; } = new();
    
}

public class BookingExtraDto
{
    public string ExtraName { get; set; } = string.Empty;
    public decimal PriceAtBooking { get; set; }
    public int     Quantity       { get; set; }
    public decimal TotalPrice     { get; set; }
}