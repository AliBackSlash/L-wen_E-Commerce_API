using Löwen.Domain.Layer_Dtos.Cart;
using Löwen.Domain.Layer_Dtos.Order;

namespace Löwen.Application.Features.CartFeature.Commands.UpdateCartItemQuantity;

public record UpdateCartItemQuantityCommand(string cartId,string productId,short quantity) : ICommand;
