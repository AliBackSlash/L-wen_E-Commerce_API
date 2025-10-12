namespace Löwen.Domain.Layer_Dtos.Cart;

public class GetCartItemDto
{
    public required string ProductImageUrl { get; set; }
    public required string ProductName { get; set; }
    public decimal Price { get; set; }
    public short Quantity { get; set; }
}