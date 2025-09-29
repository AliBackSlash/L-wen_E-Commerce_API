namespace Löwen.Domain.Layer_Dtos.Cart;

public class CartItemsDto
{
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; }
    public short Quantity { get; set; }
}
