using Löwen.Domain.ErrorHandleClasses;
using Löwen.Domain.Layer_Dtos.WishList;
using Löwen.Domain.Pagination;

namespace Löwen.Domain.Abstractions.IServices.IEntitiesServices;

public interface IWishlistService 
{
    Task<Result> AddAsync(Wishlist entity, CancellationToken ct);
    Task<Result> DeleteAsync(Guid userId, Guid productId, CancellationToken ct);
    Task<bool> IsFoundAsync(Guid userId, Guid productId, CancellationToken ct);
    Task<PagedResult<UserWishlistDto>> GetUserWishlist(Guid userId, PaginationParams prm, CancellationToken ct);
}
