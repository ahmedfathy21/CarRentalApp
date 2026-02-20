namespace CarRentalApp.Domain.Exceptions;

public class CarNotFoundException : DomainException
{
    public CarNotFoundException(int carId)
        : base("CAR_NOT_FOUND", $"Car with id {carId} not found") { }
}

public class CarNotAvailableException : DomainException
{
    public CarNotAvailableException(int carId)
        : base("CAR_NOT_AVAILABLE", $"Car with id {carId} is not available") { }
}

public class CarUnderMaintenanceException : DomainException
{
    public CarUnderMaintenanceException(int carId)
        : base("CAR_UNDER_MAINTENANCE", $"Car with ID {carId} is currently under maintenance.") { }
}

public class InvalidCarDataException : DomainException
{
    public InvalidCarDataException(string reason)
        : base("INVALID_CAR_DATA", $"Invalid car data: {reason}") { }
}

public class InvalidMileageUpdateException : DomainException
{
    public InvalidMileageUpdateException(int currentMileage, int newMileage)
        : base("INVALID_MILEAGE_UPDATE",
            $"Mileage cannot be reduced from {currentMileage} to {newMileage}.") { }
}

public class InvalidCarRateException : DomainException
{
    public InvalidCarRateException(decimal rate)
        : base("INVALID_CAR_RATE", $"Daily rate must be greater than zero. Received: {rate}.") { }
}
