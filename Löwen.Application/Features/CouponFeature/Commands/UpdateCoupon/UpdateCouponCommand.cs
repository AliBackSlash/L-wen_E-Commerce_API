using Löwen.Domain.Layer_Dtos.Cart;
using Löwen.Domain.Layer_Dtos.Order;

namespace Löwen.Application.Features.CouponFeature.Commands.UpdateCoupon;
public record UpdateCouponCommand(string Id, string? CouponId, DiscountType? DiscountType,
    decimal? DiscountValue, DateTime? StartDate, DateTime? EndDate, bool? IsActive, int? UsageLimit) : ICommand;
