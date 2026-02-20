namespace CarRentalApp.Domain.Exceptions;

public class DomainException :Exception
{
    public string Code { get; }     // Machine-readable code for logging/API responses
    
    protected DomainException(string code, string message) : base(message)
    {
        Code = code;
    }
    
}