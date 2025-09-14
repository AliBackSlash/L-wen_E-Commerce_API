namespace Löwen.Domain.Abstractions.IServices.IEntitiesServices;

public interface IProductService : IBasRepository<Product,Guid>
{
    Task<bool> IsFound(Guid Id,CancellationToken ct);
}
