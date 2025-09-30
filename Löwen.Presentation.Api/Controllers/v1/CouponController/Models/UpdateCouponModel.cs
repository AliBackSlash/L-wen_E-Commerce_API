namespace Löwen.Presentation.Api.Controllers.v1.CouponController.Models;

public record UpdateCouponModel(string CouponId, string? Code, DiscountType? DiscountType,
    decimal? DiscountValue, DateTime? StartDate, DateTime? EndDate, bool? IsActive, int? UsageLimit);
