namespace Löwen.Domain.Entities;

// ProductImages Table (Many-to-Many junction table)
public class ProductImage
{
    public Guid ProductVariantId { get; set; }
    public Guid ImageId { get; set; }

    // Navigation properties
    public ProductVariant? ProductVariant { get; set; }
    public Image? Image { get; set; }
}
