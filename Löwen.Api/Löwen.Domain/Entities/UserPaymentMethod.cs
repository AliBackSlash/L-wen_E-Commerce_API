namespace Löwen.Domain.Entities;

public class UserPaymentMethod
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? Method { get; set; }
    public string? LogoPath { get; set; }

    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}
