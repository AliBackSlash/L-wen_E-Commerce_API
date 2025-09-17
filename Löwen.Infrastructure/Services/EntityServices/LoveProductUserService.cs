using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.ErrorHandleClasses;
using Microsoft.EntityFrameworkCore;

namespace Löwen.Infrastructure.Services.EntityServices;

public class LoveProductUserService(AppDbContext context) : ILoveProductUserService
{
    public async Task<Result> AddAsync(LoveProductUser entity, CancellationToken ct)
    {
        try
        {
            await context.LovesProductUser.AddAsync(entity, ct);
            await context.SaveChangesAsync(ct);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("WishlistService.Add", ex.Message, ErrorType.InternalServer));
        }
    }

    public async Task<Result> DeleteAsync(Guid userId, Guid productId, CancellationToken ct)
    {
        var wishlist = await context.LovesProductUser.Where(x => x.UserId == userId && x.ProductId == productId).FirstOrDefaultAsync();
        if (wishlist == null)
            return Result.Failure(new Error("WishlistService.Delete", "no wishlist found", ErrorType.Conflict));

        try
        {
            context.LovesProductUser.Remove(wishlist);
            await context.SaveChangesAsync(ct);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("WishlistService.Add", ex.Message, ErrorType.InternalServer));
        }
    }
}