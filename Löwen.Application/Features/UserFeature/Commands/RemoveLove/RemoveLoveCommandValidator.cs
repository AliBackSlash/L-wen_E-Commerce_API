namespace Löwen.Application.Features.UserFeature.Commands.RemoveLove;

public class RemoveLoveCommandValidator : AbstractValidator<RemoveLoveCommand>
{
    public RemoveLoveCommandValidator()
    {
        RuleFor(x => x.userId).NotEmpty().WithMessage("Id is required")
          .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid user id");

        RuleFor(x => x.productId).NotEmpty().WithMessage("Id is required")
          .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid product id");

       
    }
}
