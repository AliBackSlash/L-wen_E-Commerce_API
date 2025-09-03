namespace Löwen.Domain.Entities;

// OrderItems Table
public class OrderItem
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public byte Quantity { get; set; }
    public decimal PriceAtPurchase { get; set; }

    // Navigation properties
    public Order? Order { get; set; }
    public Product? Product { get; set; }
}
