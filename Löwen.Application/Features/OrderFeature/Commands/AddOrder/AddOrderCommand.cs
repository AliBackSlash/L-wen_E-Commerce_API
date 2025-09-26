using Löwen.Domain.Layer_Dtos.Order;

namespace Löwen.Application.Features.UserFeature.Commands.AddOrder.AddOrder;

public record AddOrderCommand(string UserId,IEnumerable< OrderItemDto> items) : ICommand;
