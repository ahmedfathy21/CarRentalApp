namespace CarRentalApp.Domain.Exceptions;

public class CustomerNotFoundException : DomainException
{
    public CustomerNotFoundException(int customerId)
        : base("CUSTOMER_NOT_FOUND", $"Customer with ID {customerId} was not found.") { }
}

public class CustomerNotEligibleException : DomainException
{
    public CustomerNotEligibleException(string reason)
        : base("CUSTOMER_NOT_ELIGIBLE", $"Customer is not eligible to rent: {reason}") { }
}

public class CustomerBlacklistedException : DomainException
{
    public CustomerBlacklistedException(int customerId)
        : base("CUSTOMER_BLACKLISTED", $"Customer with ID {customerId} is blacklisted and cannot make bookings.") { }
}

public class InvalidCustomerDataException : DomainException
{
    public InvalidCustomerDataException(string reason)
        : base("INVALID_CUSTOMER_DATA", $"Invalid customer data: {reason}") { }
}
