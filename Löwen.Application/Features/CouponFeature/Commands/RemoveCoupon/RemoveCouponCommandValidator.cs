namespace Löwen.Application.Features.CouponFeature.Commands.RemoveCoupon;

public class RemoveCouponCommandValidator : AbstractValidator<RemoveCouponCommand>
{
  public RemoveCouponCommandValidator()
  {
        RuleFor(x => x.CouponId)
       .NotEmpty().WithMessage("CouponId is required")
       .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid CouponId");
    }
}
