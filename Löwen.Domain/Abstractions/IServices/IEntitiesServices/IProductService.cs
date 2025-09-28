using Löwen.Domain.Entities.EntityForMapFunctionsResultOnly.Product;
using Löwen.Domain.ErrorHandleClasses;
using Löwen.Domain.Layer_Dtos.Discount;
using Löwen.Domain.Pagination;

namespace Löwen.Domain.Abstractions.IServices.IEntitiesServices;

public interface IProductService : IBasRepository<Product,Guid>
{
    Task<bool> IsFound(Guid Id,CancellationToken ct);
    Task<Result<PagedResult<GetProductResult>>> GetProductsPaged(PaginationParams prm, CancellationToken ct);
    Task<Result<PagedResult<GetProductResult>>> GetAllProductPagedForRegisteredUsers(Guid userId, PaginationParams prm, CancellationToken ct);
}

public interface IDiscountService : IBasRepository<Discount,Guid>
{
    Task<bool> IsFound(Guid Id, CancellationToken ct);
    Task<Result<PagedResult<DiscountDto>>> GetAllPaged(PaginationParams prm, CancellationToken ct);
    Task<Result> AssignDiscountToProduct(Guid discountId, Guid productId, CancellationToken ct);
    Task<Result> RemoveDiscountFromProduct(Guid discountId, Guid productId, CancellationToken ct);

}