namespace Löwen.Application.Features.CartFeature.Queries.GetCartByUser;

public class GetCartByUserQueryValidator : AbstractValidator<GetCartByUserQuery>
{
  public GetCartByUserQueryValidator()
  {
        RuleFor(x => x.userId)
            .NotEmpty().WithMessage("Id is required")
            .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid user id");
        RuleFor(x => x.PageNumber)
              .NotEmpty()
              .WithMessage("PageNumber is required");

        RuleFor(x => x.PageSize)
         .NotEmpty()
         .WithMessage("PageSize is required")
         .Must(x => x <= 255).WithMessage("Max page size of products is 255");
    }
}
