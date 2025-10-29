namespace Löwen.Domain.Abstractions.IServices.IEntitiesServices;

public interface IProductCategoryService : IBasRepository<ProductCategory, Guid>
{
    Task<Guid?> GetCategoryIdByCategoryName(string categoryName, CancellationToken ct);
    Task<bool> IsFound(Guid Id,CancellationToken ct);
}
