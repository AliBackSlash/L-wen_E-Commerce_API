namespace Löwen.Application.Features.CartFeature.Queries.GetCartByUser;

public class GetCartByUserQueryValidator : AbstractValidator<GetCartByUserQuery>
{
  public GetCartByUserQueryValidator()
  {
        RuleFor(x => x.userId)
            .NotEmpty().WithMessage("Id is required")
            .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid  user id");
   
  }
}
