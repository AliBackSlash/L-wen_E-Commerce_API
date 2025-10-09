
namespace Löwen.Application.Features.AdminFeature.Commands.Product.RemoveProductVariant;

public class RemoveProductVariantCommandValidator
    : AbstractValidator<RemoveProductVariantCommand>
{
    public RemoveProductVariantCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("ProductId is required")
        .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid Id");
       
    }
}
