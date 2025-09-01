namespace Löwen.Domain.Entities;

public class Cart
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}
