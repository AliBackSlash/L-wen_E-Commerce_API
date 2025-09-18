using Löwen.Domain.ErrorHandleClasses;

namespace Löwen.Domain.Abstractions.IServices.IEntitiesServices;

public interface ILoveProductUserService
{
    Task<Result> AddAsync(LoveProductUser entity, CancellationToken ct);
    Task<Result> DeleteAsync(Guid userId, Guid productId, CancellationToken ct);
    Task<bool> IsFoundAsync(Guid userId, Guid productId, CancellationToken ct);
}
