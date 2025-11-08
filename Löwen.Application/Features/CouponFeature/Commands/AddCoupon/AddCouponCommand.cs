using Löwen.Application.Messaging.ICommand;
using Löwen.Domain.Layer_Dtos.Cart;
using Löwen.Domain.Layer_Dtos.Order;

namespace Löwen.Application.Features.CouponFeature.Commands.AddCoupon;
public record AddCouponCommand(string? Code, DiscountType DiscountType,
    decimal? DiscountValue, DateTime StartDate, DateTime EndDate, bool IsActive, int? UsageLimit) : ICommand;
