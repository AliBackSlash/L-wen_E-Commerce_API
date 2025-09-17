namespace Löwen.Domain.Entities;

// ProductCategories Table
public class ProductCategory
{
    public Guid Id { get; set; }
    public string? Category { get; set; }
    public char Gender { get; set; }

    // Navigation property
    public ICollection<Product> Products { get; set; } = [];
}
