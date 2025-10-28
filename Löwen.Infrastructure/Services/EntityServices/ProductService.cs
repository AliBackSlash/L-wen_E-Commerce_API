using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.ConfigurationClasses.StaticFilesHelpersClasses;
using Löwen.Domain.Entities;
using Löwen.Domain.Entities.EntityForMapFunctionsResultOnly.Product;
using Löwen.Domain.ErrorHandleClasses;
using Löwen.Domain.Layer_Dtos.Product;
using Löwen.Domain.Pagination;
using Löwen.Infrastructure.EFCore.Context.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.IO;

namespace Löwen.Infrastructure.Services.EntityServices;

public class ProductService(AppDbContext _context) : BasRepository<Product, Guid>(_context), IProductService
{
    public async Task<Result> AddProductVariantAsync(Guid productId, ProductVariantDto productVariantDto, CancellationToken ct)
    {
        try
        {
            await _context.ProductVariants.AddAsync(new ProductVariant
            {
                ProductId = productId,
                SizeId = productVariantDto.SizeId,
                ColorId = productVariantDto.ColorId,
                Price = productVariantDto.Price,
                StockQuantity = productVariantDto.StockQuantity,
            },ct);
            await _context.SaveChangesAsync(ct);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error($"IProductService.AddProductVariant", ex.InnerException.Message, ErrorType.InternalServer));
        }
    }
    public async Task<Result> UpdateProductVariantAsync(Guid Id, UpdateProductVariantDto productVariantDto, CancellationToken ct)
    {
        var pv = await _context.ProductVariants.Where(x => x.Id == Id).FirstOrDefaultAsync(ct);
        if (pv is null)
            return Result.Failure(new Error("IProductService.UpdateProductVariant", "Variant not found", ErrorType.Conflict));
        try
        {
            pv.SizeId = productVariantDto.SizeId ?? pv.SizeId;
            pv.ColorId = productVariantDto.ColorId ?? pv.ColorId;
            pv.Price = productVariantDto.Price ?? pv.Price;
            pv.StockQuantity = productVariantDto.StockQuantity ?? pv.StockQuantity;

            _context.ProductVariants.Update(pv);
            await _context.SaveChangesAsync(ct);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error($"IProductService.UpdateProductVariant", ex.Message, ErrorType.InternalServer));
        }
    }
    public async Task<Result> DeleteProductVariantAsync(Guid productVId, CancellationToken ct)
    {
        var pv = await _context.ProductVariants.Where(x => x.Id == productVId).FirstOrDefaultAsync(ct);
        if (pv is null)
            return Result.Failure(new Error("IProductService.DeleteProductVariant", "Variant not found", ErrorType.Conflict));
        try
        {
            _context.ProductVariants.Remove(pv);
            await _context.SaveChangesAsync(ct);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("IProductService.DeleteProductVariant", ex.Message, ErrorType.InternalServer));
        }
    }

    /*
     
    Name 
    Description 
    Price 
    Status 
    LoveCount 
    PriceAfterDiscount 
    ProductImage[] 
    Reviews[];
            UserImage 
            string UserName 
            Rating 
            Review 
            CreatedAt 
     */
    public async Task<Result<GetProductByIdDto>> GetProductByIdAsync(Guid productId,CancellationToken ct)
    {
        var product = await (
            from p in _context.Products
            where p.Id == productId
            select new GetProductByIdDto
            {
                Name = p.Name,
                Description = p.Description,
                Price = p.MainPrice,
                Status = p.Status,
                LoveCount = p.LoveCount,
                PriceAfterDiscount = _context.Discounts
                    .Where(d => d.IsActive && d.EndDate > DateTime.UtcNow)
                    .Select(d => d.DiscountValue)
                    .FirstOrDefault()
            }
        ).AsNoTracking().FirstOrDefaultAsync(ct);

        if (product is null)
            return Result.Failure<GetProductByIdDto>(new Error("IProductService.GetProductByIdAsync", $"no product with id {productId}",ErrorType.Conflict));

        product.ProductImage = await  _context.Images
            .Where(i => i.ProductId == productId)
            .Select(i => i.Path)
            .AsNoTracking()
            .ToListAsync(ct);

        product.Rating = _context.ProductReviews.Where(x => x.ProductId == productId).AsNoTracking().Average(i => i.Rating);

        product.Reviews = await (
            from rev in _context.ProductReviews
            join user in _context.Users on rev.UserId equals user.Id into Rev_Users
            from user in Rev_Users.DefaultIfEmpty()
            where rev.ProductId == productId
            select new ProductReviewsDto
            {
                UserImage = user.ImagePath,
                UserName = user.UserName!,
                Rating = rev.Rating,
                Review = rev.Review,
                CreatedAt = rev.CreatedAt
            }
        ).Skip(0).Take(10).AsNoTracking().ToListAsync(ct);

        return Result.Success(product);

    }
    public Task<Result<PagedResult<GetProductDto>>> GetAllProductPagedForRegisteredUsers(Guid userId, PaginationParams prm, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<PagedResult<GetProductDto>>> GetProductsPaged(PaginationParams prm, CancellationToken ct)
    {
        var query = await _dbSet.ToListAsync();/*from p in _context.Products
                    join pd in _context.ProductDiscounts on p.Id equals pd.ProductId into productDiscounts
                    from pd in productDiscounts.DefaultIfEmpty()

                    join d in _context.Discounts on pd.DiscountId equals d.Id  into discounts
                    from d in discounts.DefaultIfEmpty() 

                    join pi in _context.ProductImages on p.Id equals pi.ProductId into productImages
                    from pi in productImages.DefaultIfEmpty()

                    join i in _context.Images on pi.ImageId equals i.Id into images
                    from i in images.DefaultIfEmpty()

                    join r in _context.ProductReviews on p.Id equals r.ProductId into reviews
                    select new
                    {
                        p.Name,
                        p.Description,
                        p.Price,
                        p.Status,
                        p.LoveCount,
                        Discount = d != null && d.IsActive == true? d.DiscountValue : 0,
                        Rating = reviews.Any() ? reviews.Average(x => x.Rating) : 0,
                        ProductImagePath = i != null ? i.Path : null
                    };*/
        var TotalCount = 0 /*await query.CountAsync(ct)*/;
        IEnumerable<GetProductDto> products = [];/* await  query.Skip(prm.Skip)
            .Take(prm.Take)
            .Select(p => new GetProductResult
            {
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Status = p.Status,
                LoveCount = p.LoveCount,
                Discount = p.Discount,
                Rating = p.Rating,
                ProductImages = p.ProductImagePath
            }).ToListAsync();*/
        return Result.Success(PagedResult<GetProductDto>.Create(products, TotalCount, prm.PageNumber, prm.Take));

    }

    public async Task<bool> IsFound(Guid Id,CancellationToken ct) => await _dbSet.AnyAsync(t => t.Id == Id,ct);

}
