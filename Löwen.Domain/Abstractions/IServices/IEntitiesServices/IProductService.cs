using Löwen.Domain.Entities.EntityForMapFunctionsResultOnly.Product;
using Löwen.Domain.ErrorHandleClasses;
using Löwen.Domain.Layer_Dtos.Product;
using Löwen.Domain.Pagination;

namespace Löwen.Domain.Abstractions.IServices.IEntitiesServices;

public interface IProductService : IBasRepository<Product,Guid>
{
    Task<bool> IsFound(Guid Id,CancellationToken ct);
    Task<Result<GetProductByIdDto>> GetProductByIdAsync(Guid ProductId, CancellationToken ct);
    Task<PagedResult<GetProductDto>> GetProductByCategoryAsync(Guid categoryId, PaginationParams prm, CancellationToken ct);
    Task<Result<PagedResult<ProductReviewsDto>>> GetProductReviewsPaged(Guid productId, PaginationParams prm, CancellationToken ct);
    Task<Result<PagedResult<GetProductDto>>> GetProductsPaged(PaginationParams prm, CancellationToken ct);
    Task<Result<PagedResult<GetProductDto>>> GetAllProductPagedForRegisteredUsers(Guid userId, PaginationParams prm, CancellationToken ct);
    Task<Result<PagedResult<GetProductsDto>>> GetAllProductPagedForRegisteredToShowForAdmins(PaginationParams prm, CancellationToken ct);
    Task<Result<PagedResult<GetProductsDto>>> GetAllProductPagedForRegisteredToShowForAdminsByIdOrName(string IdOrName,PaginationParams prm, CancellationToken ct);
    Task<Result> AddProductVariantAsync(Guid productId, ProductVariantDto productVariantDto, CancellationToken ct);
    Task<Result> UpdateProductVariantAsync(Guid Id, UpdateProductVariantDto productVariantDto, CancellationToken ct);
    Task<Result> DeleteProductVariantAsync(Guid productVId , CancellationToken ct);
}
