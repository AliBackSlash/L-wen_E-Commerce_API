namespace Löwen.Application.Features.CouponFeature.Commands.AddCoupon;

public class AddCouponCommandValidator : AbstractValidator<AddCouponCommand>
{
  public AddCouponCommandValidator()
  {
    RuleFor(c => c.Code)
        .NotEmpty().WithMessage("Coupon code is required.")
        .MaximumLength(50).WithMessage("Coupon code must not exceed 50 characters.");

    RuleFor(c => c.DiscountType)
        .IsInEnum().WithMessage("Discount type is invalid.");

    RuleFor(c => c.DiscountValue)
        .GreaterThan(0).When(c => c.DiscountValue.HasValue)
        .WithMessage("Discount value must be greater than zero if specified.");

    RuleFor(c => c.StartDate)
        .NotEmpty().WithMessage("Start date is required.");

    RuleFor(c => c.EndDate)
        .NotEmpty().WithMessage("End date is required.")
        .GreaterThan(c => c.StartDate).WithMessage("End date must be after start date.");

    RuleFor(c => c.IsActive)
        .NotNull().WithMessage("IsActive flag is required.");

    RuleFor(c => c.UsageLimit)
        .GreaterThanOrEqualTo(0).When(c => c.UsageLimit.HasValue)
        .WithMessage("Usage limit must be zero or greater if specified.");
  }
}
