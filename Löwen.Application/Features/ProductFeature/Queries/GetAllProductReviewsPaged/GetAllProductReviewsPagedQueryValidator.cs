namespace Löwen.Application.Features.ProductFeature.Queries.GetAllProductReviewsPaged;

public class GetAllProductReviewsPagedQueryValidator : AbstractValidator<GetAllProductReviewsPagedQuery>
{
    public GetAllProductReviewsPagedQueryValidator()
    {
        RuleFor(x => x.productId).NotEmpty().WithMessage("productId is required")
           .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid productId");
        RuleFor(x => x.PageNumber)
         .NotEmpty()
         .WithMessage("PageNumber is required");

        RuleFor(x => x.PageSize)
         .NotEmpty()
         .WithMessage("PageSize is required")
         .Must(x => x <= 255).WithMessage("Max page size of products is 255");
    }
}
