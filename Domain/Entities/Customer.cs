namespace CarRentalApp.Domain.Entities;

public class Customer :BaseEntity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public string? Address { get; private set; }
    public string NationalId { get; private set; }
    public string DriverLicens { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public bool IsBlackListed { get; private set; }
    
    // Linked to ASP.Net Identity User
    public string? UserId  { get; private set; }    
    
    //Navigation 
    public ICollection<Booking> Bookings { get; private set; } = new List<Booking>();

    private Customer(){}
    
    public Customer(string firstName, string lastName, string email, string phoneNumber, string address, string nationalId,DateTime dateOfBirth)
    {
        Validate(firstName, lastName, email, dateOfBirth);
        FirstName = firstName;
        LastName = lastName;
        Email = email.ToLowerInvariant();
        PhoneNumber = phoneNumber;
        NationalId = nationalId;
        DateOfBirth = dateOfBirth;
    }
    public string FullName => $"{FirstName} {LastName}";
    public int Age => DateTime.Now.Year - DateOfBirth.Year;

    public bool IsEligibleToRent()
    {
        if (IsBlackListed) return false;
        if (Age < 21) return false;
        if(string.IsNullOrEmpty(DriverLicens)) return false;
        return true;
    }

    public void SetDriverLicense(string licenseNumber)
    {
        if(string.IsNullOrWhiteSpace(licenseNumber))
            throw new ArgumentException("Driver License number is required");
        DriverLicens = licenseNumber;
        SetUpdatedAt();
    }
    public void BlackListed()
    {
        IsBlackListed = true;
        SetUpdatedAt();
    }
    public void RemoveBlackListed()
    {
        IsBlackListed = true;
        SetUpdatedAt();
    }    
    public void UpdateProfile(string firstName, string lastName,
        string phoneNumber, string? address)
    {
        if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name is required.");
        if (string.IsNullOrWhiteSpace(lastName))  throw new ArgumentException("Last name is required.");

        FirstName   = firstName;
        LastName    = lastName;
        PhoneNumber = phoneNumber;
        Address     = address;
        SetUpdatedAt();
    }
    public void LinkToUser(string userId) { UserId = userId; SetUpdatedAt(); }
    private static void Validate(string firstName, string lastName,
        string email, DateTime dateOfBirth)
    {
        if (string.IsNullOrWhiteSpace(firstName))  throw new ArgumentException("First name is required.");
        if (string.IsNullOrWhiteSpace(lastName))   throw new ArgumentException("Last name is required.");
        if (string.IsNullOrWhiteSpace(email))      throw new ArgumentException("Email is required.");
        if (dateOfBirth > DateTime.UtcNow.AddYears(-18))
            throw new ArgumentException("Customer must be at least 18 years old.");
    }
}