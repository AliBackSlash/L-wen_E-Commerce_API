using Löwen.Domain.ErrorHandleClasses;
using Löwen.Domain.Layer_Dtos.Product;
using Löwen.Domain.Pagination;

namespace Löwen.Domain.Abstractions.IServices.IEntitiesServices;

public interface IProductService : IBasRepository<Product,Guid>
{
    Task<bool> IsFound(Guid Id,CancellationToken ct);
    Task<Result<PagedResult<GetAllProductDto>>> GetProductsPaged(PaginationParams prm, CancellationToken ct);
}
