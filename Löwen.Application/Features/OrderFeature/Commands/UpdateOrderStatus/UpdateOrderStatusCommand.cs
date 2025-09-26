namespace Löwen.Application.Features.UserFeature.Commands.UpdateOrderSataus.UpdateOrderStatus;

public record UpdateOrderStatusCommand(string OrderId, OrderStatus Status) : ICommand;
