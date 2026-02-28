namespace CarRentalApp.DTOs.Car;

public class CarDto
{
    public int     Id            { get; set; }
    public string  Make          { get; set; } = string.Empty;
    public string  Model         { get; set; } = string.Empty;
    public int     Year          { get; set; }
    public string  Color         { get; set; } = string.Empty;
    public decimal DailyRate     { get; set; }
    public string  Status        { get; set; } = string.Empty;
    public string? ImageUrl      { get; set; }
    public int     Mileage       { get; set; }

    // Flattened from relationships
    public string  CategoryName  { get; set; } = string.Empty;
    public string  BranchName    { get; set; } = string.Empty;
    public string  BranchCity    { get; set; } = string.Empty;
}