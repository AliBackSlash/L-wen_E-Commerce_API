namespace Löwen.Domain.Entities;

// Products Table
public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public short StockQuantity { get; set; }
    public byte Status { get; set; }
    public Guid CategoryId { get; set; }
    public Guid TagId { get; set; }

    // Navigation properties
    public ProductCategory? Category { get; set; }
    public ProductTag? Tag { get; set; }
    public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    public ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();
    public ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
    public ICollection<ProductDiscount> ProductDiscounts { get; set; } = new List<ProductDiscount>();

}
