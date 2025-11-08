using Löwen.Application.Messaging.IQuery;
using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.ConfigurationClasses.Pagination;
using Löwen.Domain.Pagination;
using Microsoft.Extensions.Options;

namespace Löwen.Application.Features.CouponFeature.Queries.GetCouponByCode;

internal class GetCouponByCodeQueryHandler(ICouponService couponService)
    : IQueryHandler<GetCouponByCodeQuery, GetCouponQueryResponse>
{
    public async Task<Result<GetCouponQueryResponse>> Handle(GetCouponByCodeQuery command, CancellationToken ct)
    {
        var coupon = await  couponService.GetCouponByCode(command.Code, ct);

        if (coupon is null)
           return Result.Failure<GetCouponQueryResponse>(new Error("ICouponService.GetCouponByCode", $"no Coupon with code {command.Code} found", ErrorType.Conflict));

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
