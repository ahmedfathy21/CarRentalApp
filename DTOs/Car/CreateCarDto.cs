namespace CarRentalApp.DTOs.Car;

public class CreateCarDto
{
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Color { get; set; } = string.Empty;
        public decimal DailyRate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int BranchId { get; set; }   
}