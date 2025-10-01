using Löwen.Domain.ErrorHandleClasses;
using Löwen.Domain.Layer_Dtos.Delivery;
using Löwen.Domain.Layer_Dtos.Order;
using Löwen.Domain.Pagination;

namespace Löwen.Domain.Abstractions.IServices.IEntitiesServices;

public interface IOrderService : IBasRepository<Order, Guid>
{
    Task<bool> IsFound(Guid Id, CancellationToken ct);
    Task<Result<OrderDetailsDto>> GetOrderDetails(Guid Id, CancellationToken ct);
    Task<Result<PagedResult<OrderDetailsDto>>> GetOrdersForUser(Guid userId,PaginationParams parm, CancellationToken ct);
    Task<Result<PagedResult<OrderDetailsDto>>> GetAllOrders(PaginationParams parm, CancellationToken ct);
    Task<Result> AssignedOrdersToDelivery(IEnumerable<DeliveryOrder> dto, CancellationToken ct);

}

