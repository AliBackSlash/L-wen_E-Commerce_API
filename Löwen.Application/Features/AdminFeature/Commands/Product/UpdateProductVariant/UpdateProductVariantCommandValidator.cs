
namespace Löwen.Application.Features.AdminFeature.Commands.Product.UpdateProductVariant;

public class UpdateProductVariantCommandValidator
    : AbstractValidator<UpdateProductVariantCommand>
{
    public UpdateProductVariantCommandValidator()
    {
        RuleFor(x => x.PVId).NotEmpty().WithMessage("Id is required")
        .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid Id");
        RuleFor(x => x.ColorId)
         .Must(x => x == null || Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid ColorId");
        RuleFor(x => x.SizeId)
            .Must(x => x == null || Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid ProductId");

    }
}
