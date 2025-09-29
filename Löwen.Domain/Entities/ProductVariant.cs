namespace Löwen.Domain.Entities;

public class ProductVariant
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }
    public Guid ColorId { get; set; }
    public Guid? SizeId { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }

    // Navigation
    public Product Product { get; set; } = null!;
    public Color Color { get; set; } = null!;
    public Size? Size { get; set; }
    public ICollection<ProductImage> ProductImages { get; set; } = [];

}
