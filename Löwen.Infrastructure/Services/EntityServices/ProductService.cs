using Löwen.Application.Features.ProductFeature.Queries;
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
    public async Task<Result<GetProductByIdDto>> GetProductByIdAsync(Guid productId,CancellationToken ct)
    {
        var product = await (
            from p in _context.Products
            where p.Id == productId
            let rate = _context.ProductReviews.Where(x => x.ProductId == p.Id).AsNoTracking().Average(i => i.Rating)
            let disInfoIFound = _context.Discounts
                  .Where(d => d.IsActive && d.EndDate > DateTime.UtcNow)
                  .Select(d => new { d.DiscountValue, d.DiscountType }).FirstOrDefault()
            let images = _context.Images
            .Where(i => i.ProductId == p.Id).OrderByDescending(x => x.IsMain)
            .Select(i => i.Path).ToList()
            select new GetProductByIdDto
            {

                Name = p.Name,
                Description = p.Description,
                Price = p.MainPrice,
                Status = p.Status,
                LoveCount = p.LoveCount,
                Rating = rate == null ? 0 : rate,
                Discount = disInfoIFound.DiscountValue ?? null,
                DiscountType = disInfoIFound.DiscountType == null ? null : disInfoIFound.DiscountType,
                ProductImage = images ?? null,

            }
            ).AsNoTracking().FirstOrDefaultAsync(ct);
          if (product is null)
            return Result.Failure<GetProductByIdDto>(new Error("IProductService.GetProductByIdAsync", $"no product with id {productId}",ErrorType.Conflict));
      
        return Result.Success(product);

    }
    public async Task<PagedResult<GetProductDto>> GetProductByCategoryAsync(Guid categoryId,PaginationParams prm, CancellationToken ct)
    {
        var query = 
            from p in _context.Products
            join i in _context.Images on p.Id equals i.ProductId
            where p.CategoryId == categoryId
            where i.IsMain == true
            let rate = _context.ProductReviews.Where(x => x.ProductId == p.Id).Average(i => i.Rating)
            let disInfoIFound = _context.Discounts
                  .Where(d => d.IsActive && d.EndDate > DateTime.UtcNow)
                  .Select(d => new { d.DiscountValue, d.DiscountType }).FirstOrDefault()
                  
            select new GetProductDto
            {
                ProductId = p.Id,
                Name = p.Name,
                Description = p.Description!.Substring(0, 30),
                Price = p.MainPrice,
                Status = p.Status,
                LoveCount = p.LoveCount,
                ProductImage = i.Path,
                Rating = rate == null ? 0 : rate,
                Discount = disInfoIFound.DiscountValue ?? null,
                discountType = disInfoIFound.DiscountType == null ? null : disInfoIFound.DiscountType,
            };

        var total = await query.CountAsync();
        var products = await query.AsNoTracking().Skip(prm.Skip).Take(prm.Take).OrderByDescending(x => x.Rating).ToListAsync(ct);

        return PagedResult<GetProductDto>.Create(products, total, prm.PageNumber, prm.Take);


    }
    public async Task<PagedResult<GetProductDto>> GetProductsPaged(PaginationParams prm, CancellationToken ct)
    {
        var query = 
            from p in _context.Products
            join i in _context.Images on p.Id equals i.ProductId
            where i.IsMain == true
            let rate = _context.ProductReviews.Where(x => x.ProductId == p.Id).Average(i => i.Rating)
            let disInfoIFound = _context.Discounts
                  .Where(d => d.IsActive && d.EndDate > DateTime.UtcNow)
                  .Select(d => new { d.DiscountValue, d.DiscountType }).FirstOrDefault()

            select new GetProductDto
            {
                ProductId = p.Id,
                Name = p.Name,
                Description = p.Description!.Substring(0, 30),
                Price = p.MainPrice,
                Status = p.Status,
                LoveCount = p.LoveCount,
                ProductImage = i.Path,
                Rating = rate == null ? 0 : rate,
                Discount = disInfoIFound.DiscountValue ?? null,
                discountType = disInfoIFound.DiscountType == null ? null : disInfoIFound.DiscountType,
            };

        var total = await query.CountAsync(ct);
        var products = await query.AsNoTracking().Skip(prm.Skip).Take(prm.Take).OrderByDescending(x => x.Rating).ToListAsync(ct);   
        return PagedResult<GetProductDto>.Create(products, total, prm.PageNumber, prm.Take);

    }
    public async Task<Result<PagedResult<ProductReviewsDto>>> GetProductReviewsPaged(Guid productId,PaginationParams prm, CancellationToken ct)
    {
        var query =
            from rev in _context.ProductReviews
            join user in _context.Users on rev.UserId equals user.Id into Rev_Users
            from user in Rev_Users.DefaultIfEmpty()
            where rev.ProductId == productId
            select new
            {
                UserImage = user.ImagePath,
                UserName = user.FName! + ' ' + user.LName,
                rev.Rating,
                rev.Review,
                rev.CreatedAt
            };

        var TotalCount = await query.CountAsync(ct);

        var reviews =  await  query.Skip(prm.Skip)
            .Take(prm.Take)
            .Select(p => new ProductReviewsDto
            {
                Rating = p.Rating,  
                UserName = p.UserName,
                Review = p.Review,
                CreatedAt = p.CreatedAt
                
            }).ToListAsync();
        return Result.Success(PagedResult<ProductReviewsDto>.Create(reviews, TotalCount, prm.PageNumber, prm.Take));

    }
    public async Task<bool> IsFound(Guid Id,CancellationToken ct) => await _dbSet.AnyAsync(t => t.Id == Id,ct);
    public async Task<Result<PagedResult<GetProductsDto>>> GetAllProductPagedToShowForAdmins(PaginationParams prm, CancellationToken ct)
    {
        var query = from p in _dbSet
                    join admin in _context.Users
                    on p.CreatedBy equals admin.Id
                    orderby p.LoveCount descending
                    select new
                    {
                        p.Id,
                        p.Name,
                        p.LoveCount,
                        p.MainPrice,
                        p.Status,
                        admin.UserName
                    };
        var TotalCount = await query.CountAsync(ct);

        var products = await query.Skip(prm.Skip)
            .Take(prm.Take)
            .Select(p => new GetProductsDto
            {
                ProductId = p.Id,
                Name = p.Name,
                LoveCount = p.LoveCount,
                MainPrice = p.MainPrice,
                CreatedBy = p.UserName, 
                Status = p.Status,
            }).ToListAsync();
        return Result.Success(PagedResult<GetProductsDto>.Create(products, TotalCount, prm.PageNumber, prm.Take));

    }
    public async Task<Result<PagedResult<GetProductsDto>>> GetAllProductPagedToShowForAdminsByIdOrName(string IdOrName, PaginationParams prm, CancellationToken ct)
    {
        var query = from p in _dbSet.Where(x => EF.Functions.Like(x.Id.ToString(),$"%{IdOrName}%")
                                            || EF.Functions.Like(x.Name, $"%{IdOrName}%"))
                    join admin in _context.Users
                    on p.CreatedBy equals admin.Id
                    orderby p.LoveCount descending
                    select new
                    {
                        p.Id,
                        p.Name,
                        p.LoveCount,
                        p.MainPrice,
                        p.Status,
                        admin.UserName
                    };
        var TotalCount = await query.CountAsync(ct);

        var products = await query.Skip(prm.Skip)
            .Take(prm.Take)
            .Select(p => new GetProductsDto
            {
                ProductId = p.Id,
                Name = p.Name,
                LoveCount = p.LoveCount,
                MainPrice = p.MainPrice,
                CreatedBy = p.UserName,
                Status = p.Status,
            }).ToListAsync();
        return Result.Success(PagedResult<GetProductsDto>.Create(products, TotalCount, prm.PageNumber, prm.Take));
    }
    public async Task<PagedResult<GetProductDto>> GetProductsPagedByName(string Name, PaginationParams prm, CancellationToken ct)
    {
        var query =
            from p in _context.Products.Where(x => EF.Functions.Like(x.Name, $"%{Name}%") || EF.Functions.Like(x.Tags, $"%{Name}%"))
            join i in _context.Images on p.Id equals i.ProductId
            where i.IsMain == true
            let rate = _context.ProductReviews.Where(x => x.ProductId == p.Id).Average(i => i.Rating)
            let disInfoIFound = _context.Discounts
                  .Where(d => d.IsActive && d.EndDate > DateTime.UtcNow)
                  .Select(d => new { d.DiscountValue, d.DiscountType }).FirstOrDefault()

            select new GetProductDto
            {
                ProductId = p.Id,
                Name = p.Name,
                Description = p.Description!.Substring(0, 30),
                Price = p.MainPrice,
                Status = p.Status,
                LoveCount = p.LoveCount,
                ProductImage = i.Path,
                Rating = rate == null ? 0 : rate,
                Discount = disInfoIFound.DiscountValue ?? null,
                discountType = disInfoIFound.DiscountType == null ? null : disInfoIFound.DiscountType,
            };

        var total = await query.CountAsync(ct);
        var products = await query.AsNoTracking().Skip(prm.Skip).Take(prm.Take).OrderByDescending(x => x.Rating).ToListAsync(ct);
       
        return PagedResult<GetProductDto>.Create(products, total, prm.PageNumber, prm.Take);

    }
    public async Task<PagedResult<GetProductDto>> GetProductsPagedByGender(char Gender, PaginationParams prm, CancellationToken ct)
    {
        var query = 
            from p in _context.Products
            join i in _context.Images on p.Id equals i.ProductId
            join category in _context.ProductCategories.Where(x => x.Gender == Gender) on p.CategoryId equals category.Id
            where i.IsMain == true
            let rate = _context.ProductReviews.Where(x => x.ProductId == p.Id).Average(i => i.Rating)
            let disInfoIFound = _context.Discounts
                  .Where(d => d.IsActive && d.EndDate > DateTime.UtcNow)
                  .Select(d => new { d.DiscountValue, d.DiscountType }).FirstOrDefault()

            select new GetProductDto
            {
                ProductId = p.Id,
                Name = p.Name,
                Description = p.Description!.Substring(0, 30),
                Price = p.MainPrice,
                Status = p.Status,
                LoveCount = p.LoveCount,
                ProductImage = i.Path,
                Rating = rate == null ? 0 : rate,
                Discount = disInfoIFound.DiscountValue ?? null,
                discountType = disInfoIFound.DiscountType == null ? null : disInfoIFound.DiscountType,
            };
        

        var total = await query.CountAsync(ct);
        var products = await query.AsNoTracking().Skip(prm.Skip).Take(prm.Take).OrderByDescending(x => x.Rating).ToListAsync(ct);
       
        return PagedResult<GetProductDto>.Create(products, total, prm.PageNumber, prm.Take);

    }
    public async Task<PagedResult<GetProductDto>> GetMostLovedProductsPaged(PaginationParams prm, CancellationToken ct)
    {

        var query = from p in _context.Products
                    join i in _context.Images on p.Id equals i.ProductId
                    where i.IsMain == true
                    let rate = _context.ProductReviews.Where(x => x.ProductId == p.Id).Average(i => i.Rating)
                    let disInfoIFound = _context.Discounts
                         .Where(d => d.IsActive && d.EndDate > DateTime.UtcNow)
                         .Select(d => new { d.DiscountValue, d.DiscountType }).FirstOrDefault()

                    select new GetProductDto
                    {
                        ProductId = p.Id,
                        Name = p.Name,
                        Description = p.Description!.Substring(0, 30),
                        Price = p.MainPrice,
                        Status = p.Status,
                        LoveCount = p.LoveCount,
                        ProductImage = i.Path,
                        Rating = rate == null ? 0 : rate,
                        Discount = disInfoIFound.DiscountValue ?? null,
                        discountType = disInfoIFound.DiscountType == null ? null : disInfoIFound.DiscountType,
                    };

        var total = await query.CountAsync(ct);
        var products = await query.AsNoTracking().Skip(prm.Skip).Take(prm.Take).OrderByDescending(x => x.LoveCount).ToListAsync(ct);
        
        return PagedResult<GetProductDto>.Create(products, total, prm.PageNumber, prm.Take);

    }
    public Task<Result<PagedResult<GetProductDto>>> GetAllProductPagedForRegisteredUsers(Guid userId, PaginationParams prm, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
