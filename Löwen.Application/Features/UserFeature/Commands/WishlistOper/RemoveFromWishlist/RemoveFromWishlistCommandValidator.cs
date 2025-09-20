namespace Löwen.Application.Features.UserFeature.Commands.WishlistOper.RemoveFromWishlist;

public class RemoveFromWishlistCommandValidator : AbstractValidator<RemoveFromWishlistCommand>
{
    public RemoveFromWishlistCommandValidator()
    {
        RuleFor(x => x.userId).NotEmpty().WithMessage("user Id is required")
          .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid user id");

        RuleFor(x => x.productId).NotEmpty().WithMessage("product Id is required")
          .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid product id");

       
    }
}
