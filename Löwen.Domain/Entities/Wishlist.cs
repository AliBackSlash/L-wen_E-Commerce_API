namespace Löwen.Domain.Entities;

// Wishlist Table
public class Wishlist
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }

    // Navigation properties
    public Product? Product { get; set; }
}
