namespace Löwen.Application.Features.CouponFeature.Commands.RemoveCouponFromOrder;

public class RemoveCouponFromOrderCommandValidator : AbstractValidator<RemoveCouponFromOrderCommand>
{
  public RemoveCouponFromOrderCommandValidator()
  {
        RuleFor(x => x.CouponCode)
            .NotEmpty().WithMessage("CouponCode is required");
        RuleFor(x => x.OrderId)
            .NotEmpty().WithMessage("OrderId is required")
            .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid OrderId");
    }
}
