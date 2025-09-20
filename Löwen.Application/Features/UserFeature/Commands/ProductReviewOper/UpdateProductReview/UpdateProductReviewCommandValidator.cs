namespace Löwen.Application.Features.UserFeature.Commands.ProductReviewOper.UpdateProductReview;

public class UpdateProductReviewCommandValidator : AbstractValidator<UpdateProductReviewCommand>
{
    public UpdateProductReviewCommandValidator()
    {
        RuleFor(x => x.productReviewId).NotEmpty().WithMessage("Id is required")
          .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid productReviw id");

        RuleFor(x => x.Review).NotEmpty().WithMessage("Review is required").Length(1,1000).WithMessage("Review must be between 1 and 1000 letters");

        RuleFor(x => x.Rating)
            .Must(x => byte.TryParse(x.ToString(), out byte t) && t <= 5).WithMessage("Enter a valid number 0 => 5");
    }
}
