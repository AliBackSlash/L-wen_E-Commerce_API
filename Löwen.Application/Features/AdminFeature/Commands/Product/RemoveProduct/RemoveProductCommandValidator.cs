namespace Löwen.Application.Features.AdminFeature.Commands.Product.RemoveProduct;

public class RemoveProductCommandValidator : AbstractValidator<RemoveProductCommand>
{
    public RemoveProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required")
       .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid Id");
    }
}