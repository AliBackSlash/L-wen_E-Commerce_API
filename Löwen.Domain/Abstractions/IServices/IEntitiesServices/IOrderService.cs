using Löwen.Domain.ErrorHandleClasses;
using Löwen.Domain.Layer_Dtos.Order;

namespace Löwen.Domain.Abstractions.IServices.IEntitiesServices;

public interface IOrderService : IBasRepository<Order, Guid>
{
    Task<bool> IsFound(Guid Id, CancellationToken ct);
    Task<Result<OrderDetailsDto>> GetOrderDetails(Guid Id, CancellationToken ct);

}
public interface IOrderItemsService : IBasRepository<OrderItem, Guid>
{
    Task<OrderItem> GetOrderItem(Guid orderId, Guid productId,CancellationToken ct);
}
