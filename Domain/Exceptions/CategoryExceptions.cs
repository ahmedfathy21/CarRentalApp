namespace CarRentalApp.Domain.Exceptions;

public class InvalidCategoryDataException : DomainException
{
    public InvalidCategoryDataException(string reason)
        : base("INVALID_CATEGORY_DATA", $"Invalid category data: {reason}") { }
}
