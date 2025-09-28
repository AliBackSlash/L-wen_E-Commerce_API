namespace Löwen.Application.Features.DiscountFeature.Queries.GetById;

public class GetDiscountByIdQueryValidator : AbstractValidator<GetDiscountByIdQuery>
{
  public GetDiscountByIdQueryValidator()
  {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage("Id must be a valid GUID.");
    }
}
