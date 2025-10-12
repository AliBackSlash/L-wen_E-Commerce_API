namespace Löwen.Domain.Entities;

// Cart Table
public class Cart
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    // Navigation properties
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}
