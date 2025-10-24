using Löwen.Domain.ErrorHandleClasses;
using Löwen.Domain.Layer_Dtos.Coupon;
using Löwen.Domain.Pagination;

namespace Löwen.Domain.Abstractions.IServices.IEntitiesServices;

public interface ICouponService : IBasRepository<Coupon ,Guid>
{
    Task<Guid?> GetIdIfCouponCodeFound(string CouponCode, CancellationToken ct);
    Task<Coupon?> GetCouponByCode(string CouponCode, CancellationToken ct);
    Task<PagedResult<CouponDto>> GetAllAsync(PaginationParams parm, CancellationToken ct);
    Task<Result> ApplyCouponToOrder(Guid couponId,Guid OrderId, CancellationToken ct);
    Task<Result> RemoveCouponFromOrder(Guid couponId,Guid OrderId, CancellationToken ct);
}