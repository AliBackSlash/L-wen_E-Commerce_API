namespace Löwen.Application.Features.ProductFeature.Queries.GetAllProductPagedForRegisteredUsers;

public class GetAllProductPagedForRegisteredUsersQueryValidator : AbstractValidator<GetAllProductPagedForRegisteredUsersQuery>
{
    public GetAllProductPagedForRegisteredUsersQueryValidator()
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
