namespace CarRentalApp.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } 
    public string? Description { get; set; }
    public decimal BaseDailyRate { get; set; } // Base pricing per category
    
    // Navigation
    public ICollection<Car> Cars { get; private set; } = new List<Car>();
    private Category() { }
    public Category(string name ,string? description, decimal baseDailyRate)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category Name is required");
        if(baseDailyRate <= 0)
            throw new ArgumentException("BaseDailyRate must be greater than zero");
        Name = name;
        Description = description;
        BaseDailyRate = baseDailyRate;
    }

    public void Update(Category category)
    {
        if(string.IsNullOrWhiteSpace(category.Name))
            throw new ArgumentException("Category Name is reqired");
        if(category.BaseDailyRate <= 0)
            throw new ArgumentException("BaseDailyRate must be greater than zero");
        Name = category.Name;
        Description = category.Description;
        BaseDailyRate = category.BaseDailyRate;
        SetUpdatedAt();
    }


}