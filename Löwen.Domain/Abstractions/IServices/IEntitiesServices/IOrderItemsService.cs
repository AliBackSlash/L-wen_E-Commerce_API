using Löwen.Domain.Layer_Dtos.Payment;

namespace Löwen.Domain.Abstractions.IServices.IEntitiesServices;

public interface IOrderItemsService : IBasRepository<OrderItem, Guid>
{
    Task<OrderItem> GetOrderItem(Guid orderId, Guid productId, CancellationToken ct);
}
