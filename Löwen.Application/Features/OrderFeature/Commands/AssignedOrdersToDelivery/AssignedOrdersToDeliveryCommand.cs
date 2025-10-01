using Löwen.Domain.Layer_Dtos.Delivery;
using Löwen.Domain.Layer_Dtos.Order;

namespace Löwen.Application.Features.OrderFeature.Commands.AssignedOrdersToDelivery;

public record AssignedOrdersToDeliveryCommand(IEnumerable<DeliveryOrderDto> orders) : ICommand;
