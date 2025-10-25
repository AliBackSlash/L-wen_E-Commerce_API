using Löwen.Domain.ErrorHandleClasses;
using Löwen.Domain.Layer_Dtos.Discount;
using Löwen.Domain.Pagination;

namespace Löwen.Domain.Abstractions.IServices.IEntitiesServices;

public interface IDiscountService : IBasRepository<Discount,Guid>
{
    public Task<bool> IsHaveSameDisName(string DisName, CancellationToken ct);
    Task<bool> IsFound(Guid Id, CancellationToken ct);
    Task<PagedResult<DiscountDto>> GetAllPaged(PaginationParams prm, CancellationToken ct);
    Task<Result> AssignDiscountToProduct(Guid discountId, Guid productId, CancellationToken ct);
    Task<Result> RemoveDiscountFromProduct(Guid discountId, Guid productId, CancellationToken ct);

}