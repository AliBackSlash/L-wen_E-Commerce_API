using Löwen.Domain.Entities.EntityForMapFunctionsResultOnly.Product;
using Löwen.Domain.ErrorHandleClasses;
using Löwen.Domain.Pagination;

namespace Löwen.Domain.Abstractions.IServices.IEntitiesServices;

public interface IProductService : IBasRepository<Product,Guid>
{
    Task<bool> IsFound(Guid Id,CancellationToken ct);
    Task<Result<PagedResult<GetProductResult>>> GetProductsPaged(PaginationParams prm, CancellationToken ct);
    Task<Result<PagedResult<GetProductResult>>> GetAllProductPagedForRegisteredUsers(Guid userId, PaginationParams prm, CancellationToken ct);
}
