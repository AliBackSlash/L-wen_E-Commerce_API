using Löwen.Domain.Enums;

namespace Löwen.Domain.Entities;

// Products Table
public class Product
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public ProductStatus Status { get; set; }
    public double LoveCount {  get; set; }
    public Guid CategoryId { get; set; }
    public Guid CreatedBy { get; set; }


    // Navigation properties
    public ProductCategory? Category { get; set; }
    public ProductTag? Tag { get; set; }
    public ICollection<ProductVariant> ProductVariants { get; set; } = [];
    public ICollection<Image> Images { get; set; } = [];
    public ICollection<ProductReview> ProductReviews { get; set; } = [];
    public ICollection<CartItem> CartItems { get; set; } = [];
    public ICollection<OrderItem> OrderItems { get; set; } = [];
    public ICollection<Wishlist> Wishlists { get; set; } = [];
    public ICollection<LoveProductUser> Loves { get; set; } = [];
    public ICollection<ProductDiscount> ProductDiscounts { get; set; } = [];
}
