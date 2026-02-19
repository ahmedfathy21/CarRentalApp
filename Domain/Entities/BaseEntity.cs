namespace CarRentalApp.Domain.Entities;

public class BaseEntity
{
    public int Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public bool IsDeleted { get; private set; }

    protected BaseEntity()
    {
        CreatedAt = DateTime.UtcNow;
    }
    public void markAsDeleted() => IsDeleted = true;
    public void SetUpdatedAt() => UpdatedAt = DateTime.UtcNow;
}