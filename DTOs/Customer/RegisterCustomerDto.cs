namespace CarRentalApp.DTOs.Customer;

public class RegisterCustomerDto
{
    public string   FirstName     { get; set; } = string.Empty;
    public string   LastName      { get; set; } = string.Empty;
    public string   Email         { get; set; } = string.Empty;
    public string   PhoneNumber   { get; set; } = string.Empty;
    public string   NationalId    { get; set; } = string.Empty;
    public DateTime DateOfBirth   { get; set; }
    public string?  Address       { get; set; }
    public string   Password      { get; set; } = string.Empty;  // for Identity
}