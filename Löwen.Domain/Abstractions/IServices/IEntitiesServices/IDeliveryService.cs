using Löwen.Domain.ErrorHandleClasses;
using Löwen.Domain.Layer_Dtos.Delivery;
using Löwen.Domain.Pagination;

namespace Löwen.Domain.Abstractions.IServices.IEntitiesServices;

public interface IDeliveryService
{
    Task<PagedResult<GetAssignedOrdersDto>> GetAssignedOrdersAsync(Guid userId, PaginationParams parm, CancellationToken ct);
}

