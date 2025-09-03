namespace Löwen.Domain.Entities;

// ProductTags Table
public class ProductTag
{
    public Guid Id { get; set; }
    public string? Tag { get; set; }

    // Navigation property
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
