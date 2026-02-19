using CarRentalApp.Domain.Enums;
namespace CarRentalApp.Domain.Entities;

public class Branch :BaseEntity
{
    public string Name          { get; private set; }   // e.g. "Cairo Airport Branch"
    public string PhoneNumber   { get; private set; }
    public string Address       { get; private set; }
    public string City          { get; private set; }
    public bool IsActive        { get; private set; }

    // Navigation
    public ICollection<Car> Cars { get; private set; } = new List<Car>();

    private Branch() { }

    public Branch(string name, string phoneNumber, string address, string city)
    {
        if (string.IsNullOrWhiteSpace(name))    throw new ArgumentException("Branch name is required.");
        if (string.IsNullOrWhiteSpace(address)) throw new ArgumentException("Address is required.");
        if (string.IsNullOrWhiteSpace(city))    throw new ArgumentException("City is required.");

        Name        = name;
        PhoneNumber = phoneNumber;
        Address     = address;
        City        = city;
        IsActive    = true;
    }

    public void Deactivate() { IsActive = false; SetUpdatedAt(); }
    public void Activate()   { IsActive = true;  SetUpdatedAt(); }

    public void UpdateInfo(string name, string phoneNumber, string address, string city)
    {
        Name        = name;
        PhoneNumber = phoneNumber;
        Address     = address;
        City        = city;
        SetUpdatedAt();
    }
    
}