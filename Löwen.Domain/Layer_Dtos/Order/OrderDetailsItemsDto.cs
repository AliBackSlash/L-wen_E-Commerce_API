namespace Löwen.Domain.Layer_Dtos.Order;

public class OrderDetailsItemsDto
{
    public Guid ProductId { get; set; }
    public required string ProductName { get; set; }
    public byte Quantity { get; set; }
    public decimal Price { get; set; }
    public required string Path { get; set; }
}