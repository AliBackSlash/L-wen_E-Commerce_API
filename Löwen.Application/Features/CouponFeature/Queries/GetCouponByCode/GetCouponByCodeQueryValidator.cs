namespace Löwen.Application.Features.CouponFeature.Queries.GetCouponByCode;

public class GetCouponByCodeQueryValidator : AbstractValidator<GetCouponByCodeQuery>
{
  public GetCouponByCodeQueryValidator()
  {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code is required");

    }
}
