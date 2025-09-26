namespace Löwen.Application.Features.UserFeature.Queries.GetAllOrders;

public class GetAllOrdersQueryValidator : AbstractValidator<GetAllOrdersQuery>
{
    public GetAllOrdersQueryValidator()
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
