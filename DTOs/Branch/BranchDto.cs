namespace CarRentalApp.DTOs.Branch;

public class BranchDto
{
    public int?    Id          { get; set; }   // null = create, set = update
    public string  Name        { get; set; } = string.Empty;
    public string  PhoneNumber { get; set; } = string.Empty;
    public string  Address     { get; set; } = string.Empty;
    public string  City        { get; set; } = string.Empty;
    public bool    IsActive    { get; set; }
    public int     TotalCars   { get; set; }   // computed — only on output
}