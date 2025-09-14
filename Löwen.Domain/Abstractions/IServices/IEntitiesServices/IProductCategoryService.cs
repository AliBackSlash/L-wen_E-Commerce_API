namespace Löwen.Domain.Abstractions.IServices.IEntitiesServices;

public interface IProductCategoryService : IBasRepository<ProductCategory, Guid>
{
    Task<bool> IsFound(Guid Id,CancellationToken ct);
}
