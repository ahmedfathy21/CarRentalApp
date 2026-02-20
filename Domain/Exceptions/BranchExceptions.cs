namespace CarRentalApp.Domain.Exceptions;

public class InvalidBranchDataException : DomainException
{
    public InvalidBranchDataException(string reason)
        : base("INVALID_BRANCH_DATA", $"Invalid branch data: {reason}") { }
}
