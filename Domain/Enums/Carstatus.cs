namespace CarRentalApp.Domain.Enums;

public enum CarStatus
{
    Available        = 1,   // Car is in the branch, ready to be booked
    Rented           = 2,   // Car is currently out with a customer
    UnderMaintenance = 3,   // Car is in the garage, cannot be booked
    Retired          = 4    // Car is permanently removed from the fleet
}
