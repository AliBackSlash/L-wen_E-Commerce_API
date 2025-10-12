namespace Löwen.Application.Features.CartFeature.Commands.AddToCart;

public class AddToCartCommandValidator : AbstractValidator<AddToCartCommand>
{
  public AddToCartCommandValidator()
  {
    RuleFor(x => x.UserId)
        .NotEmpty().WithMessage("Id is required")
        .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid user id");
    RuleFor(x => x.ProductId)
        .NotEmpty().WithMessage("ProductId is required")
        .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid ProductId");
  }
}
