using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.ConfigurationClasses.Pagination;
using Löwen.Domain.Pagination;
using Microsoft.Extensions.Options;

namespace Löwen.Application.Features.CouponFeature.Queries.GetAllCoupons;

internal class GetAllCouponsQueryHandler(ICouponService couponService, IOptions<PaginationSettings> options)
    : IQueryHandler<GetAllCouponsQuery, PagedResult<GetCouponQueryResponse>>
{
    public async Task<Result<PagedResult<GetCouponQueryResponse>>> Handle(GetAllCouponsQuery command, CancellationToken ct)
    {
        var coupon = await  couponService.GetAllAsync(new PaginationParams
        {
            maxPageSize = options.Value.maxPageSize,
            PageNumber = command.PageNumber,
            Take = command.PageSize,
        }, ct);

       
        return Result.Success(PagedResult<GetCouponQueryResponse>.Create(coupon.Items.Select(i => new GetCouponQueryResponse
        {
            Code = i.Code,
            DiscountType = i.DiscountType,
            DiscountValue = i.DiscountValue,
            StartDate = i.StartDate,
            EndDate = i.EndDate,
            IsActive = i.IsActive,
            UsageLimit = i.UsageLimit,

        })
        ,coupon.TotalCount,coupon.PageNumber,coupon.PageSize));
    }
}
