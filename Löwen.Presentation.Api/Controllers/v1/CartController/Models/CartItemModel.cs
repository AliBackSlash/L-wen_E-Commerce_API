using Löwen.Domain.Layer_Dtos.Cart;

namespace Löwen.Presentation.Api.Controllers.v1.CartController.Models;

public class CartItemModel
{
  public IEnumerable<CartItemsDto> items { get; set; } = [];
}
