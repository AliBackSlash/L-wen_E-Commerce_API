namespace Löwen.Application.Features.CartFeature.Commands.UpdateCartItemQuantity;

public class RemoveFromCartItemCommandValidator : AbstractValidator<RemoveFromCartItemCommand>
{
  public RemoveFromCartItemCommandValidator()
  {
    RuleFor(x => x.cartId)
        .NotEmpty().WithMessage("Id is required")
        .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid  cart id");
    RuleFor(x => x.productId)
        .NotEmpty().WithMessage("product Id is required")
        .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid product id");
    
        RuleFor(x => x.quantity)
        .NotEmpty().WithMessage("quantity is required")
        .Must(x => x >= 1).WithMessage("quantity must be greater than 0");

   
  }
}
