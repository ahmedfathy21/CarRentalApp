namespace CarRentalApp.DTOs.Car;

public class UpdateCarDto
{
    public int     Id          { get; set; }
    public string  Make        { get; set; } = string.Empty;
    public string  Model       { get; set; } = string.Empty;
    public int     Year        { get; set; }
    public string  Color       { get; set; } = string.Empty;
    public int     Seats       { get; set; }
    public decimal DailyRate   { get; set; }
    public string? ImageUrl    { get; set; }
    public int     CategoryId  { get; set; }
    public int     BranchId    { get; set; }    
}