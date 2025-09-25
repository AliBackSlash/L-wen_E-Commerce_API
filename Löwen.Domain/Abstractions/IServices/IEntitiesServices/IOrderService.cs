namespace Löwen.Domain.Abstractions.IServices.IEntitiesServices;

public interface IOrderService : IBasRepository<Order, Guid>
{
    Task<bool> IsFound(Guid Id, CancellationToken ct);

}
public interface IOrderItems : ICollectionBasRepository<OrderItem, Guid>
{

}
