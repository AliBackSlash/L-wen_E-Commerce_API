using Löwen.Domain.ErrorHandleClasses;

namespace Löwen.Domain.Abstractions.IServices.IEntitiesServices;

public interface IWishlistService 
{
    Task<Result> AddAsync(Wishlist entity, CancellationToken ct);
    Task<Result> DeleteAsync(Guid userId, Guid productId, CancellationToken ct);
    Task<bool> IsFoundAsync(Guid userId, Guid productId, CancellationToken ct);
}
