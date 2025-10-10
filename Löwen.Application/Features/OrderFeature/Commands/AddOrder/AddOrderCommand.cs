using Löwen.Domain.Layer_Dtos.Order;

namespace Löwen.Application.Features.OrderFeature.Commands.AddOrder.AddOrder;

public record AddOrderCommand(string deliveryId, IEnumerable<OrderItemDto> items) : ICommand;
