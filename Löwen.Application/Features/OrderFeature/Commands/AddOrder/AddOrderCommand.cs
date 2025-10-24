using Löwen.Domain.Layer_Dtos.Order;

namespace Löwen.Application.Features.OrderFeature.Commands.AddOrder.AddOrder;

public record AddOrderCommand(string CustomerId, IEnumerable<OrderItemDto> items) : ICommand;
