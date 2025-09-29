namespace Löwen.Domain.Layer_Dtos.Cart;

public class GetCartItemDto
{
    public required string ProductImage { get; set; }
    public short Quantity { get; set; }
}