using Löwen.Domain.Entities;
using Löwen.Domain.Layer_Dtos.Order;

namespace Löwen.Presentation.Api.Controllers.v1.OrderController.Models;

public record OrderItemModel(IEnumerable<OrderItemDto> orders);
