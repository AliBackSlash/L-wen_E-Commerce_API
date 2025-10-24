using Löwen.Domain.ConfigurationClasses.ApiSettings;
using Löwen.Domain.ConfigurationClasses.StaticFilesHelpersClasses;
using Löwen.Domain.Layer_Dtos.Cart;
using Löwen.Domain.Layer_Dtos.Coupon;
using Microsoft.Extensions.Options;

namespace Löwen.Application.Features.CouponFeature.Queries;

public class GetCouponQueryResponse
{
    public string? Code { get; set; }
    public DiscountType DiscountType { get; set; }
    public decimal? DiscountValue { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    public int? UsageLimit { get; set; }


    public static GetCouponQueryResponse map(CouponDto dto)
        => new GetCouponQueryResponse
        {
            Code = dto.Code,
            DiscountType = dto.DiscountType,  
            DiscountValue = dto.DiscountValue,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            IsActive = dto.IsActive,
            UsageLimit = dto.UsageLimit,
            
        };

    public static IEnumerable<GetCouponQueryResponse> map(IEnumerable<CouponDto> dtos) => dtos.Select(i => map(i)).ToList();

}
