namespace Löwen.Application.Features.UserFeature.Commands.WishlistOper.RemoveProductReview;

public class RemoveProductReviewCommandValidator : AbstractValidator<RemoveProductReviewCommand>
{
    public RemoveProductReviewCommandValidator()
    {
       
        RuleFor(x => x.productReviewId).NotEmpty().WithMessage("product Review id is required")
          .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid product review id");

       
    }
}
