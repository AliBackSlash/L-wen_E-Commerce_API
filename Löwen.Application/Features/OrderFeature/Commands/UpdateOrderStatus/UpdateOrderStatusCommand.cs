using Löwen.Application.Messaging.ICommand;

namespace Löwen.Application.Features.OrderFeature.Commands.UpdateOrderSataus.UpdateOrderStatus;

public record UpdateOrderStatusCommand(string OrderId, OrderStatus Status) : ICommand;
