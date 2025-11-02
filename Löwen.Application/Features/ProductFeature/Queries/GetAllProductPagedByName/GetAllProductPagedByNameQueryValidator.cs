namespace Löwen.Application.Features.ProductFeature.Queries.GetAllProductPagedByName;

public class GetAllProductPagedByNameQueryValidator : AbstractValidator<GetAllProductPagedByNameQuery>
{
    public GetAllProductPagedByNameQueryValidator()
    {
        RuleFor(x => x.Name)
         .NotEmpty()
         .WithMessage("Name is required");

        RuleFor(x => x.PageNumber)
         .NotEmpty()
         .WithMessage("PageNumber is required");

        RuleFor(x => x.PageSize)
         .NotEmpty()
         .WithMessage("PageSize is required")
         .Must(x => x <= 255).WithMessage("Max page size of products is 255");
    }
}
