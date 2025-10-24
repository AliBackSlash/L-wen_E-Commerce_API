using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.Entities;
using Löwen.Domain.ErrorHandleClasses;
using Löwen.Domain.Layer_Dtos.Coupon;
using Löwen.Domain.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Löwen.Infrastructure.Services.EntityServices;

public class CouponService(AppDbContext _context) : BasRepository<Coupon,Guid>(_context), ICouponService
{

    public async Task<Result> ApplyCouponToOrder(Guid CouponId, Guid OrderId, CancellationToken ct)
    {
        var entity = new OrderCoupon
        {
            CouponId = CouponId,
            OrderId = OrderId
        };
        try
        {
            await _context.AddAsync(entity, ct);
            await _context.SaveChangesAsync(ct);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error($"ICouponService.ApplyCouponToOrder", ex.Message, ErrorType.Conflict));
        }
    }

    public async Task<PagedResult<CouponDto>> GetAllAsync(PaginationParams parm, CancellationToken ct)
    {
        var query = from coupon in _dbSet select new 
        {
            coupon.Code,
            coupon.DiscountType,
            coupon.DiscountValue,
            coupon.StartDate,
            coupon.EndDate,
            coupon.UsageLimit,
            coupon.IsActive
        };

        int count = await query.CountAsync();

        var items = await query.Take(parm.Take).Skip(parm.Skip).Select(coupon => new CouponDto
        {
            Code = coupon.Code,
            DiscountType = coupon.DiscountType,
            DiscountValue = coupon.DiscountValue,
            StartDate = coupon.StartDate,
            EndDate = coupon.EndDate,
            UsageLimit = coupon.UsageLimit,
            IsActive = coupon.IsActive
        }).AsNoTracking().ToListAsync();

        return PagedResult<CouponDto>.Create(items, count, parm.PageNumber, parm.Take);
    }

    public async Task<Coupon?> GetCouponByCode(string CouponCode, CancellationToken ct)
        => await _dbSet.Where(x => x.Code == CouponCode).FirstOrDefaultAsync();

    public async Task<Guid?> GetIdIfCouponCodeFound(string CouponCode, CancellationToken ct)
        => await _context.Coupons.Where(t => t.Code == CouponCode).Select(x => x.Id).FirstOrDefaultAsync(ct);

    public async Task<Result> RemoveCouponFromOrder(Guid couponId, Guid OrderId, CancellationToken ct)
    {
        var entity = _context.OrderCoupons.Where(x => x.CouponId == couponId && x.OrderId == OrderId).FirstOrDefaultAsync();

        if (entity is null)
            return Result.Failure(new Error($"ICouponService.RemoveCouponFromOrder", "OrderCoupons not found", ErrorType.Conflict));
        try
        {
            await _context.AddAsync(entity, ct);
            await _context.SaveChangesAsync(ct);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error($"ICouponService.RemoveCouponFromOrder", ex.Message, ErrorType.Conflict));
        }
    }
}