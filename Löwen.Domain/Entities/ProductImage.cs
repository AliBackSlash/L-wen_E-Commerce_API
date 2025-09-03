namespace Löwen.Domain.Entities;

// ProductImages Table (Many-to-Many junction table)
public class ProductImage
{
    public Guid ProductId { get; set; }
    public Guid ImageId { get; set; }

    // Navigation properties
    public Product? Product { get; set; }
    public Image? Image { get; set; }
}
