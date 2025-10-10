namespace Löwen.Application.Features.OrderFeature.Commands.UpdateOrderItem.UpdateOrderItem;

public record UpdateOrderItemCommand(string orderId,string? deliveryId , string productId, byte? Quantity, decimal? PriceAtPurchase) : ICommand;
