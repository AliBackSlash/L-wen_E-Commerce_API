namespace Löwen.Application.Features.UserFeature.Commands.Love.UpdateOrderStatus;

public record UpdateOrderStatusCommand(string OrderId, OrderStatus Status) : ICommand;
