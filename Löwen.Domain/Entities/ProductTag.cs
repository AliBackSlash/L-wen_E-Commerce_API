namespace Löwen.Domain.Entities;

// ProductTags Table
public class ProductTag
{
    public Guid Id { get; set; }
    public string? Tag { get; set; }
    public Guid ProductId { get; set; }

    // Navigation property
    public Product? Product { get; set; }
}
