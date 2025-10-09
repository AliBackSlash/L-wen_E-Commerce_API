namespace Löwen.Domain.Abstractions.IServices.IEntitiesServices;

public interface IProductTagService : IBasRepository<ProductTag, Guid>
{
    Task<ProductTag> GetByProductIdAsync(Guid productId, CancellationToken ct);

}
