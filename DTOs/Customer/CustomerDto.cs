namespace CarRentalApp.DTOs.Customer;

public class CustomerDto
{
    public int     Id            { get; set; }
    public string  FirstName     { get; set; } = string.Empty;
    public string  LastName      { get; set; } = string.Empty;
    public string  FullName      { get; set; } = string.Empty;
    public string  Email         { get; set; } = string.Empty;
    public string  PhoneNumber   { get; set; } = string.Empty;
    public string? Address       { get; set; }
    public int     Age           { get; set; }
    public bool    HasLicense    { get; set; }
    public int     TotalBookings { get; set; }  // computed
}