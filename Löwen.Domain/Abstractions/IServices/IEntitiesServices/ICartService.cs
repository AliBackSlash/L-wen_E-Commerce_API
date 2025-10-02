using Löwen.Domain.ErrorHandleClasses;
using Löwen.Domain.Layer_Dtos.Cart;
using Löwen.Domain.Pagination;

namespace Löwen.Domain.Abstractions.IServices.IEntitiesServices;

public interface ICartService : IBasRepository<Cart,Guid>
{
    Task<bool> IsFound(Guid Id, CancellationToken ct);
    Task<Result> RemoveCartItem(Guid cartId, Guid productId, CancellationToken ct);
    Task<Result> UpdateCartItemQuantity(Guid cartId, Guid productId,short quantity, CancellationToken ct);
    Task<PagedResult<GetCartItemDto>> GetCartForUser(Guid userId, CancellationToken ct);


}
