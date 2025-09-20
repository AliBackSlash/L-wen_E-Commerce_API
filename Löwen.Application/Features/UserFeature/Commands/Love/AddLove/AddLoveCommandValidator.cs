namespace Löwen.Application.Features.UserFeature.Commands.Love.AddLove;

public class AddLoveCommandValidator : AbstractValidator<AddLoveCommand>
{
    public AddLoveCommandValidator()
    {
        RuleFor(x => x.userId).NotEmpty().WithMessage("Id is required")
          .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid user id");

        RuleFor(x => x.productId).NotEmpty().WithMessage("Id is required")
          .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid product id");

       
    }
}
