namespace Löwen.Application.Features.ProductFeature.Queries.GetAllProductPaged;

public class GetAllProductPagedQueryValidator : AbstractValidator<GetAllProductPagedQuery>
{
    public GetAllProductPagedQueryValidator()
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
