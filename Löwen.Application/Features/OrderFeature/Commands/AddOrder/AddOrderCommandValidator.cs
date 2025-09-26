namespace Löwen.Application.Features.UserFeature.Commands.AddOrder.AddOrder;

public class AddOrderCommandValidator : AbstractValidator<AddOrderCommand>
{
  public AddOrderCommandValidator()
  {
    RuleFor(x => x.UserId).NotEmpty().WithMessage("Id is required")
      .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid user id");

  }
}
