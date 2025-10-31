namespace Löwen.Application.Features.AdminFeature.Queries.GetProductsByIdOrName;

public class GetProductsByIdOrNameQueryValidator : AbstractValidator<GetProductsByIdOrNameQuery>
{
    public GetProductsByIdOrNameQueryValidator()
    {
        RuleFor(x => x.IdOrName)
         .NotEmpty()
         .WithMessage("Id Or Name is required");

        RuleFor(x => x.PageNumber)
         .NotEmpty()
         .WithMessage("PageNumber is required");

        RuleFor(x => x.PageSize)
         .NotEmpty()
         .WithMessage("PageSize is required")
         .Must(x => x <= 255).WithMessage("Max page size of products is 255");


    }
}
