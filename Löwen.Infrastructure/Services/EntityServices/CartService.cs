using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.ErrorHandleClasses;
using Löwen.Domain.Layer_Dtos.Cart;
using Löwen.Domain.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Löwen.Infrastructure.Services.EntityServices;

public class CartService(AppDbContext _context) : BasRepository<Cart, Guid>(_context), ICartService
{
    public Task<PagedResult<GetCartItemDto>> GetCartForUser(Guid userId, CancellationToken ct)
    {
        //var query = from c in  _context.Carts
        //            join ci in _context.CartItems on c.Id equals ci.CartId
        //            join p in _context.Products on ci.ProductId equals p.Id
        //           // join pi in _context.ProductImages on p.Id equals pi.ProductId
        //            join i in _context.Images on pi.ImageId equals i.Id orderby i.IsMain descending 
        //            where c.UserId == userId 
        //            select new
        //            {
        //                ProductImage = i.
        //            }
                    throw new NotImplementedException();
    }

    public async Task<bool> IsFound(Guid UserId, CancellationToken ct) => await _dbSet.AnyAsync(t => t.UserId == UserId, ct);

    public async Task<Result> RemoveCartItem(Guid cartId, Guid productId, CancellationToken ct)
    {
        var ci = await _context.CartItems.Where(c => c.CartId == cartId && c.ProductId == productId).FirstOrDefaultAsync();
       
        if (ci is null)
            return Result.Failure(new Error($"ICartService.RemoveCartItem", "cart item not found", ErrorType.Conflict));

        try
        {
            _context.CartItems.Remove(ci);
            await _context.SaveChangesAsync(ct);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error($"ICartService.RemoveCartItem", ex.Message, ErrorType.InternalServer));
        }
    }

    public async Task<Result> UpdateCartItemQuantity(Guid cartId, Guid productId, short quantity, CancellationToken ct)
    {
        var ci = await _context.CartItems.Where(c => c.CartId == cartId && c.ProductId == productId).FirstOrDefaultAsync();

        if (ci is null)
            return Result.Failure(new Error($"ICartService.UpdateCartItemQuantity", "cart item not found", ErrorType.Conflict));

        try
        {
            ci.Quantity = quantity;
            _context.CartItems.Update(ci);
            await _context.SaveChangesAsync(ct);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error($"ICartService.UpdateCartItemQuantity", ex.Message, ErrorType.InternalServer));
        }
    }
}