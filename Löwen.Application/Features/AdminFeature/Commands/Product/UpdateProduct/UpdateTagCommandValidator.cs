namespace Löwen.Application.Features.AdminFeature.Commands.Product.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required")
            .Must(x => Guid.TryParse(x,out _)).WithMessage("Enter a valid Guid id");
        RuleFor(x => x.Tag).NotEmpty().WithMessage("Tag is not null")
              .MaximumLength(100).WithMessage("the max Tag length is 100 chars");

    }
}
