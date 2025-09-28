namespace Löwen.Application.Features.DiscountFeature.Queries.GetAll;

public class GetAllDiscountQueryValidator : AbstractValidator<GetAllDiscountQuery>
{
  public GetAllDiscountQueryValidator()
  {
        RuleFor(x => x.PageNumber)
        .NotEmpty()
        .WithMessage("PageNumber is required");

        RuleFor(x => x.PageSize)
         .NotEmpty()
         .WithMessage("PageSize is required")
         .Must(x => x <= 255).WithMessage("Max page size of products is 255");
    }
}
