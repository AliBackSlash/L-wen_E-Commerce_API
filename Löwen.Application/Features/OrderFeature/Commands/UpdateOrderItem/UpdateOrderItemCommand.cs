namespace Löwen.Application.Features.UserFeature.Commands.UpdateOrderItem.UpdateOrderItem;

public record UpdateOrderItemCommand(string orderId, string productId, byte? Quantity, decimal? PriceAtPurchase) : ICommand;
