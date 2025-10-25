using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.ErrorHandleClasses;
using Löwen.Domain.Layer_Dtos.Discount;
using Löwen.Domain.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Löwen.Infrastructure.Services.EntityServices;

public class DiscountService(AppDbContext _context) : BasRepository<Discount, Guid>(_context), IDiscountService
{
    public async Task<bool> IsHaveSameDisName(string DisName, CancellationToken ct) => await _dbSet.AnyAsync(x => x.Name == DisName);
    public async Task<Result> AssignDiscountToProduct(Guid discountId, Guid productId, CancellationToken ct)
    {
        try
        {
            await _context.ProductDiscounts.AddAsync(new ProductDiscount
            {
                DiscountId = discountId,
                ProductId = productId,
            }, ct);
            await _context.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
           return Result.Failure(new Error("IDiscountService.AssignDiscountToProduct", ex.Message, ErrorType.Conflict));
        }
    }

    public async Task<PagedResult<DiscountDto>> GetAllPaged(PaginationParams prm, CancellationToken ct)
    {
        var query = from dis in _dbSet select dis;

        
        var totalCount = await query.CountAsync();
        var items = await query.Select(d => new DiscountDto
        {
            Id = d.Id,
            Name = d.Name,
            DiscountType = d.DiscountType,
            DiscountValue = d.DiscountValue,
            StartDate = d.StartDate,
            EndDate = d.EndDate,
            IsActive = d.IsActive
        }).Skip(prm.Skip).Take(prm.Take).ToListAsync();
        return PagedResult<DiscountDto>.Create(items, totalCount, prm.PageNumber, prm.Take);
    }

    public async Task<bool> IsFound(Guid Id, CancellationToken ct) => await _dbSet.AnyAsync(t => t.Id == Id, ct);

    public async Task<Result> RemoveDiscountFromProduct(Guid discountId, Guid productId, CancellationToken ct)
    {
        var pro_dis = await _context.ProductDiscounts.Where(pd => pd.ProductId == productId && pd.DiscountId == discountId).FirstOrDefaultAsync();

        if (pro_dis is null)
            return Result.Failure(new Error("IDiscountService.RemoveDiscountFromProduct", "discount on product not found", ErrorType.Conflict));
        try
        {
            _context.ProductDiscounts.Remove(pro_dis);
            await _context.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("IDiscountService.RemoveDiscountFromProduct", ex.Message, ErrorType.Conflict));
        }
    }
}
