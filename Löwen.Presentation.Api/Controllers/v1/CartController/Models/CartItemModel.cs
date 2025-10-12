using Löwen.Domain.Layer_Dtos.Cart;

namespace Löwen.Presentation.Api.Controllers.v1.CartController.Models;

public class CartItemModel
{
    public required string ProductId { get; set; }
    public short Quantity { get; set; }
}
