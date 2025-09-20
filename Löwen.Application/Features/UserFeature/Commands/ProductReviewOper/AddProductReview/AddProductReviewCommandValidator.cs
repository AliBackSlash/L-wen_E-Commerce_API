namespace Löwen.Application.Features.UserFeature.Commands.ProductReviewOper.AddProductReview;

public class AddProductReviewCommandValidator : AbstractValidator<AddProductReviewCommand>
{
    public AddProductReviewCommandValidator()
    {
        RuleFor(x => x.userId).NotEmpty().WithMessage("user Id is required")
          .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid user id");

        RuleFor(x => x.productId).NotEmpty().WithMessage("product Id is required")
          .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid product id");

        RuleFor(x => x.Review).NotEmpty().WithMessage("Review is required").Length(1,1000).WithMessage("Review must be between 1 and 1000 letters");

        RuleFor(x => x.Rating)
            .Must(x => byte.TryParse(x.ToString(), out byte t) && t <= 5).WithMessage("Enter a valid number 0 => 5");
    }
}
