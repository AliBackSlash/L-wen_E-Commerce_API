using Löwen.Domain.ErrorHandleClasses;

namespace Löwen.Domain.Abstractions.IServices.IEntitiesServices;

public interface ICouponService : IBasRepository<Coupon ,Guid>
{
    Task<Guid?> GetIdIfCouponCodeFound(string CouponCode, CancellationToken ct);
    Task<Result> ApplyCouponToOrder(Guid couponId,Guid OrderId, CancellationToken ct);
    Task<Result> RemoveCouponFromOrder(Guid couponId,Guid OrderId, CancellationToken ct);
}