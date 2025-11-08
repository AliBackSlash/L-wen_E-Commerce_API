using Löwen.Application.Messaging.IQuery;
using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.ConfigurationClasses.Pagination;
using Löwen.Domain.Pagination;
using Microsoft.Extensions.Options;

namespace Löwen.Application.Features.CouponFeature.Queries.GetCouponById;

internal class GetCouponByIdQueryHandler(ICouponService couponService)
    : IQueryHandler<GetCouponByIdQuery, GetCouponQueryResponse>
{
    public async Task<Result<GetCouponQueryResponse>> Handle(GetCouponByIdQuery command, CancellationToken ct)
    {
        var coupon = await  couponService.GetByIdAsync(Guid.Parse(command.CouponId), ct);

        if (coupon is null)
           return Result.Failure<GetCouponQueryResponse>(new Error("ICouponService.GetByIdAsync", $"no Coupon with Id {command.CouponId} found", ErrorType.Conflict));

        return Result.Success(new GetCouponQueryResponse
        {
            Code = coupon.Code,
            DiscountType = coupon.DiscountType,
            DiscountValue = coupon.DiscountValue,
            StartDate = coupon.StartDate,
            EndDate = coupon.EndDate,
            UsageLimit = coupon.UsageLimit,
            IsActive = coupon.IsActive
        });
    }
}
