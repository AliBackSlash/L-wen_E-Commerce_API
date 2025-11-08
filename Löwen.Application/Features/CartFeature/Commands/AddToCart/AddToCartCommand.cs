using Löwen.Application.Messaging.ICommand;
using Löwen.Domain.Layer_Dtos.Cart;
using Löwen.Domain.Layer_Dtos.Order;

namespace Löwen.Application.Features.CartFeature.Commands.AddToCart;

public record AddToCartCommand(string UserId, string ProductId, short Quantity = 1) : ICommand;
