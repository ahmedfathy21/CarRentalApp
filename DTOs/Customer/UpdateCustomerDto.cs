namespace CarRentalApp.DTOs.Customer;

public class UpdateCustomerDto
{
    public int     Id            { get; set; }
    public string  FirstName     { get; set; } = string.Empty;
    public string  LastName      { get; set; } = string.Empty;
    public string  PhoneNumber   { get; set; } = string.Empty;
    public string? Address       { get; set; }
    public string? DriverLicense { get; set; }
}