using Löwen.Domain.Layer_Dtos.Cart;
using Löwen.Domain.Layer_Dtos.Order;

namespace Löwen.Application.Features.CartFeature.Commands.AddToCart;

public record AddToCartCommand(string UserId,IEnumerable<CartItemsDto> items) : ICommand;
