namespace Löwen.Application.Features.AdminFeature.Commands.Product.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required")
            .Must(x => Guid.TryParse(x,out _)).WithMessage("Enter a valid Guid id");
        RuleFor(x => x.Name).Must(x => x is null || x.Length <= 70).WithMessage("the max Product name length is 70 chars");
        RuleFor(x => x.Description).Must(x => x is null || x.Length <= 1200).WithMessage("the max Description length is 1200 chars");
        RuleFor(x => x.CategoryId).Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid Category Id");
    }
}
