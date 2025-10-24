namespace Löwen.Application.Features.CouponFeature.Queries.GetCouponById;

public class GetCouponByIdQueryValidator : AbstractValidator<GetCouponByIdQuery>
{
  public GetCouponByIdQueryValidator()
  {
        RuleFor(x => x.CouponId)
            .NotEmpty().WithMessage("Id is required")
            .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid user id");

    }
}
