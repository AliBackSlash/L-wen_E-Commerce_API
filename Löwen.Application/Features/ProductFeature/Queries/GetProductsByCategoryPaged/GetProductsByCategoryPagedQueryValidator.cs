namespace Löwen.Application.Features.ProductFeature.Queries.GetProductsByCategoryPaged;

public class GetProductsByCategoryPagedQueryValidator : AbstractValidator<GetProductsByCategoryPagedQuery>
{
    public GetProductsByCategoryPagedQueryValidator()
    {
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(x => x.PageNumber)
         .NotEmpty()
         .WithMessage("PageNumber is required");

        RuleFor(x => x.PageSize)
         .NotEmpty()
         .WithMessage("PageSize is required")
         .Must(x => x <= 255).WithMessage("Max page size of products is 255");
    }
}
