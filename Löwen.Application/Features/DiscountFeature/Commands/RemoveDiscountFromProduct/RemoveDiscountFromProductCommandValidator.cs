namespace Löwen.Application.Features.DiscountFeature.Commands.RemoveDiscountFromProduct;

public class RemoveDiscountFromProductCommandValidator : AbstractValidator<RemoveDiscountFromProductCommand>
{
  public RemoveDiscountFromProductCommandValidator()
  {
        RuleFor(x => x.discountId)
            .NotEmpty().WithMessage("DiscountId is required.")
            .Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage("DiscountId must be a valid GUID.");

        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("ProductId is required.")
            .Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage("ProductId must be a valid GUID.");
    }
}
