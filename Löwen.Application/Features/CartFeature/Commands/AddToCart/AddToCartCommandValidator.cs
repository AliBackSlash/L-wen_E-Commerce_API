namespace Löwen.Application.Features.CartFeature.Commands.AddToCart;

public class AddToCartCommandValidator : AbstractValidator<AddToCartCommand>
{
  public AddToCartCommandValidator()
  {
    RuleFor(x => x.UserId)
        .NotEmpty().WithMessage("Id is required")
        .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid user id");

    RuleFor(x => x.items)
        .Must(items => items != null && items.Any()).WithMessage("At least one item is required in the cart");
  }
}
