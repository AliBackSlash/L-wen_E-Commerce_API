
namespace Löwen.Application.Features.AdminFeature.Commands.Product.AddProduct;

public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    public AddProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Product name is required")
    .MaximumLength(70).WithMessage("the max Product name length is 70 chars");

        RuleFor(x => x.Description).NotEmpty().WithMessage("Product Description is required")
            .MaximumLength(1200).WithMessage("the max Description length is 1200 chars");

        RuleFor(x => x.VariantDtos)
            .NotNull().WithMessage("Variants are required")
            .Must(v => v != null && v.Any()).WithMessage("At least one variant is required")
            .Must(v => v == null || v.Count() <= 10).WithMessage("the max number of variants is 10");

        RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Id is required")
    .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid Category Id");

        RuleFor(x => x.CreatedBy).NotEmpty().WithMessage("CreatedBy is required")
    .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid CreatedBy Id");
    }
}
