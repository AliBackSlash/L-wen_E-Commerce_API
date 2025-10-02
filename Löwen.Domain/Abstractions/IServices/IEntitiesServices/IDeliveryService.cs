using Löwen.Domain.ErrorHandleClasses;
using Löwen.Domain.Pagination;

namespace Löwen.Domain.Abstractions.IServices.IEntitiesServices;

public interface IDeliveryService
{
    Task<Result> GetAssignedOrders(Guid userId, PaginationParams parm, CancellationToken ct);
}

