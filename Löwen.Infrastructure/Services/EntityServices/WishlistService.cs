using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.ErrorHandleClasses;
using Löwen.Domain.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Löwen.Infrastructure.Services.EntityServices;

public class WishlistService(AppDbContext context) : IWishlistService
{
    public async Task<Result> AddAsync(Wishlist entity, CancellationToken ct)
    {
        try
        {
            await context.Wishlist.AddAsync(entity, ct);
            await context.SaveChangesAsync(ct);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("WishlistService.Add", ex.Message, ErrorType.InternalServer));
        }
    }

    public async Task<Result> DeleteAsync(Guid userId,Guid productId, CancellationToken ct)
    {
        var wishlist = await context.Wishlist.Where(x => x.UserId == userId && x.ProductId == productId).FirstOrDefaultAsync(ct);
        if (wishlist == null)
            return Result.Failure(new Error("WishlistService.Delete", "no wishlist found", ErrorType.Conflict));

        try
        {
            context.Wishlist.Remove(wishlist);
            await context.SaveChangesAsync(ct);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("WishlistService.Add", ex.Message, ErrorType.InternalServer));
        }
    }

    public async Task<bool> IsFoundAsync(Guid userId, Guid productId, CancellationToken ct) 
        => await context.Wishlist.Where(x => x.UserId == userId && x.ProductId == productId).FirstOrDefaultAsync(ct) is not null;


}
