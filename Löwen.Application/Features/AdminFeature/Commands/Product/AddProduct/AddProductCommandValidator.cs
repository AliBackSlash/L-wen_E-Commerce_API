
namespace Löwen.Application.Features.AdminFeature.Commands.Product.AddProduct;

public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    public AddProductCommandValidator()
    {
        RuleFor(x => x.Tag).NotEmpty().WithMessage("Tag is not null")
            .MaximumLength(100).WithMessage("the max Tag length is 100 chars");
      
        RuleFor(x => x.productId).NotEmpty().WithMessage("Id is required")
            .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid for productId");
    }
}
