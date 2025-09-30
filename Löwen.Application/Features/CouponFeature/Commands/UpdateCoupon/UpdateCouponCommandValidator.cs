namespace Löwen.Application.Features.CouponFeature.Commands.UpdateCoupon;

public class UpdateCouponCommandValidator : AbstractValidator<UpdateCouponCommand>
{
  public UpdateCouponCommandValidator()
  {
        RuleFor(x => x.CouponId)
       .NotEmpty().WithMessage("CouponId is required")
       .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid CouponId");

        RuleFor(c => c.CouponId)
            .NotEmpty().When(c => c.CouponId != null)
            .WithMessage("Coupon code is required.")
            .MaximumLength(50).When(c => c.CouponId != null)
            .WithMessage("Coupon code must not exceed 50 characters.");

        RuleFor(c => c.DiscountType)
            .IsInEnum().When(c => c.DiscountType != null)
            .WithMessage("Discount type is invalid.");

        RuleFor(c => c.DiscountValue)
            .GreaterThan(0).When(c => c.DiscountValue.HasValue)
            .WithMessage("Discount value must be greater than zero if specified.");

        RuleFor(c => c.StartDate)
            .NotEmpty().When(c => c.StartDate != null)
            .WithMessage("Start date is required.");

        RuleFor(c => c.EndDate)
            .NotEmpty().When(c => c.EndDate != null)
            .WithMessage("End date is required.")
            .GreaterThan(c => c.StartDate).When(c => c.EndDate != null && c.StartDate != null)
            .WithMessage("End date must be after start date.");

        RuleFor(c => c.UsageLimit)
            .GreaterThanOrEqualTo(0).When(c => c.UsageLimit.HasValue)
            .WithMessage("Usage limit must be zero or greater if specified.");
    }
}
