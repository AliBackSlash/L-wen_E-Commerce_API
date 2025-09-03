namespace Löwen.Domain.Entities;

// Images Table
public class Image
{
    public Guid Id { get; set; }
    public string? Path { get; set; }

    // Navigation property
    public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
}
