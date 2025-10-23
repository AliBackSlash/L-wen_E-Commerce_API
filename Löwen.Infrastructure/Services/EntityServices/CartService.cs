using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.ConfigurationClasses.ApiSettings;
using Löwen.Domain.ConfigurationClasses.StaticFilesHelpersClasses;
using Löwen.Domain.ErrorHandleClasses;
using Löwen.Domain.Layer_Dtos.Cart;
using Löwen.Domain.Pagination;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Löwen.Infrastructure.Services.EntityServices;

public class CartService(AppDbContext _context,IOptions<StaticFilesSettings> FilesSettings ,IOptions<ApiSettings> apiSettings) : BasRepository<Cart, Guid>(_context), ICartService
{
    public async Task<Result> AddToCartAsync(CartItem cartItem, CancellationToken ct)
    {
        try
        {
            await _context.CartItems.AddAsync(cartItem, ct);
            await _context.SaveChangesAsync(ct);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error($"ICartService.AddToCart", ex.Message, ErrorType.InternalServer));
        }
    }

    public async Task<Result<PagedResult<GetCartItemDto>>> GetCartForUser(Guid userId,PaginationParams pram, CancellationToken ct)
    {
        //make sure that return the main image only and one ProductVariants
        var query = from cart in _context.Carts
                    join cartItem in _context.CartItems on cart.Id equals cartItem.CartId
                    join product in _context.Products on cartItem.ProductId equals product.Id
                    where cart.UserId == userId
                    orderby cartItem.CreatedAt descending
                    select new
                    {
                        ProductName = product.Name,
                        ProductImageUrl = (from image in _context.Images
                                           where product.Id == image.ProductId
                                           where image.IsMain == true
                                           select image.Path)
                                            .Take(1).Single(),
                        Price = (from productVariant in _context.ProductVariants
                                 where product.Id == productVariant.ProductId
                                 select productVariant.Price).Take(1).Single(),
                        cartItem.Quantity
                    };


        var s = query.ToQueryString();
        var totalCount = await query.CountAsync();
        var items = await query.Select(i => new GetCartItemDto
        {
            ProductName = i.ProductName,
            ProductImageUrl = Path.Combine( FilesSettings.Value.ProductImages_FileName , i.ProductImageUrl),
            Price = i.Price,
            Quantity = i.Quantity,
        }).Skip(pram.Skip).Take(pram.Take).ToListAsync();

        return Result.Success(PagedResult<GetCartItemDto>.Create(items, totalCount, pram.PageNumber, pram.Take));
    }

    public async Task<Guid?> GetCartIdByUserId(Guid userId, CancellationToken ct)
        => await _dbSet.Where(x => x.UserId == userId).Select(x => x.Id).FirstOrDefaultAsync(ct);

    public async Task<bool> IsUserHasCart(Guid UserId, CancellationToken ct) => await _dbSet.AnyAsync(t => t.UserId == UserId, ct);

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