namespace Löwen.Domain.Entities;

// CartItems Table
public class CartItem
{
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; }
    public short Quantity { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public Cart? Cart { get; set; }
    public Product? Product { get; set; }
    
}
