namespace CarRentalApp.DTOs.Category;

public class CategoryDto
{
    public int?    Id           { get; set; }   // null = create, set = update
    public string  Name         { get; set; } = string.Empty;
    public string? Description  { get; set; }
    public decimal BaseDailyRate{ get; set; }
    public int     TotalCars    { get; set; }   // computed — only on output
}