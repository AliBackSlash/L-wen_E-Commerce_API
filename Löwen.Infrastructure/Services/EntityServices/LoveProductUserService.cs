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
            return Result.Failure(new Error("LoveProductUserService.Add", ex.Message, ErrorType.InternalServer));
        }
    }

    public async Task<Result> DeleteAsync(Guid userId, Guid productId, CancellationToken ct)
    {
        var LoveProduct = await context.LovesProductUser.Where(x => x.UserId == userId && x.ProductId == productId).FirstOrDefaultAsync();
        if (LoveProduct == null)
            return Result.Failure(new Error("LoveProductUserService.Delete", "no Love Product found", ErrorType.Conflict));

        try
        {
            context.LovesProductUser.Remove(LoveProduct);
            await context.SaveChangesAsync(ct);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("WishlistService.Add", ex.Message, ErrorType.InternalServer));
        }
    }
    public async Task<bool> IsFoundAsync(Guid userId, Guid productId, CancellationToken ct)
        => await context.LovesProductUser.Where(x => x.UserId == userId && x.ProductId == productId).FirstOrDefaultAsync(ct) is not null;
}