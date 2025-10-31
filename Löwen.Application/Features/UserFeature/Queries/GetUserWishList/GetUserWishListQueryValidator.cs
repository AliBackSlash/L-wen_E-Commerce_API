namespace Löwen.Application.Features.UserFeature.Queries.GetUserWishList;

public class GetUserWishListQueryValidator : AbstractValidator<GetUserWishListQuery>
{
    public GetUserWishListQueryValidator()
    {
        RuleFor(x => x.userId).NotEmpty().WithMessage("Id is required")
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
