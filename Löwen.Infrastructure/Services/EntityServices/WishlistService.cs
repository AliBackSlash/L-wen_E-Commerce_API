using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.ErrorHandleClasses;
using Löwen.Domain.Layer_Dtos.WishList;
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
    /*  public Guid WithlistId { get; set; }
        public required string ProductName { get; set; }
        public required string Image { get; set; }
        public double Price {  get; set; }*/
    public async Task<PagedResult<UserWishlistDto>> GetUserWishlist(Guid userId, PaginationParams prm, CancellationToken ct)
    {
        var query = from wishl in context.Wishlist
                    join product in context.Products on wishl.ProductId equals product.Id
                    join image in context.Images on product.Id equals image.ProductId
                    where wishl.UserId == userId
                    where image.IsMain == true
                    orderby wishl.CreatedAt ascending
                    select new 
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        Image = image.Path!,
                        Price = product.MainPrice
                    };
        var a = query.ToQueryString();
        var total = await query.CountAsync();

        var wishlist = await query.Skip(prm.Skip)
                        .Take(prm.Take)
                        .Select(x => new UserWishlistDto
                         {
                        ProductId = x.ProductId ,
                        ProductName = x.ProductName ,
                        Image = x.Image !,
                        Price = x.Price 
                        })
                        .ToListAsync();

        return PagedResult<UserWishlistDto>.Create(wishlist, total, prm.PageNumber, prm.Take);

    }

    public async Task<bool> IsFoundAsync(Guid userId, Guid productId, CancellationToken ct) 
        => await context.Wishlist.Where(x => x.UserId == userId && x.ProductId == productId).FirstOrDefaultAsync(ct) is not null;


}
