using Löwen.Application.Messaging.ICommand;
using Löwen.Domain.Layer_Dtos.Cart;
using Löwen.Domain.Layer_Dtos.Order;

namespace Löwen.Application.Features.CartFeature.Commands.RemoveFromCartItem;

public record RemoveFromCartItemCommand(string cartId,string productId) : ICommand;
