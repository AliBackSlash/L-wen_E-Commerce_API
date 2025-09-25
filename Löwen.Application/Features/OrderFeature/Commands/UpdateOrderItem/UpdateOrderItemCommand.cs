namespace Löwen.Application.Features.UserFeature.Commands.Love.UpdateOrderItem;

public record UpdateOrderItemCommand(string orderId, string productId, byte? Quantity, decimal? PriceAtPurchase) : ICommand;
