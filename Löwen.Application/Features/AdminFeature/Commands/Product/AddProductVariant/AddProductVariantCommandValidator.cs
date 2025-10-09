
namespace Löwen.Application.Features.AdminFeature.Commands.Product.AddProductVariant;

public class AddProductVariantCommandValidator
    : AbstractValidator<AddProductVariantCommand>
{
    public AddProductVariantCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId is required")
        .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid ProductId");
        RuleFor(x => x.ColorId).NotEmpty().WithMessage("ColorId is required")
         .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid ColorId");
        RuleFor(x => x.SizeId)
            .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid ProductId").When(x => x != null);
        RuleFor(x => x.StockQuantity).NotEmpty().WithMessage("StockQuantity is required");
        RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required");
    }
}
