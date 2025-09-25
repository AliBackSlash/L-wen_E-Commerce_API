namespace Löwen.Domain.Layer_Dtos.Order;

public class OrderItemDto
{
    public Guid ProductId { get; set; }
    public byte Quantity { get; set; }
    public decimal PriceAtPurchase { get; set; }
}
