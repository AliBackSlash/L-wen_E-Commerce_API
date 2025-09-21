using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.Entities;
using Löwen.Domain.ErrorHandleClasses;
using Löwen.Domain.Layer_Dtos.Product;
using Löwen.Domain.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Löwen.Infrastructure.Services.EntityServices;

public class ProductService(AppDbContext _context) : BasRepository<Product, Guid>(_context), IProductService
{
    public async Task<Result<PagedResult<GetAllProductDto>>> GetProductsPaged(PaginationParams prm, CancellationToken ct)
    {
        var query = from p in _context.Products
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
                    };

        var TotalCount = await query.CountAsync(ct);
        var products = await  query.Skip(prm.Skip)
            .Take(prm.PageSize)
            .Select(p => new GetAllProductDto
            {
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Status = p.Status,
                LoveCount = p.LoveCount,
                Discount = p.Discount,
                Rating = p.Rating,
                ProductImages = p.ProductImagePath
            }).ToListAsync();
        return Result.Success(PagedResult<GetAllProductDto>.Create(products, TotalCount, prm.PageNumber, prm.PageSize));

    }

    public async Task<bool> IsFound(Guid Id,CancellationToken ct) => await _context.Products.AnyAsync(t => t.Id == Id,ct);

}
