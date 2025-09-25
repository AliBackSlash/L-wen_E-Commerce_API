using Löwen.Domain.ErrorHandleClasses;

namespace Löwen.Domain.Abstractions.IServices.IEntitiesServices;

public interface IOrderService : IBasRepository<Order, Guid>
{
    Task<bool> IsFound(Guid Id, CancellationToken ct);

}
public interface IOrderItemsService : IBasRepository<OrderItem, Guid>
{
    Task<OrderItem> GetOrderItem(Guid orderId, Guid productId,CancellationToken ct);
}
