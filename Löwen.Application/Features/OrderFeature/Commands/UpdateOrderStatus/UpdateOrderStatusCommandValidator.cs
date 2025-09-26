namespace Löwen.Application.Features.UserFeature.Commands.UpdateOrderSataus.UpdateOrderStatus;

public class UpdateOrderStatusCommandValidator : AbstractValidator<UpdateOrderStatusCommand>
{
    public UpdateOrderStatusCommandValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty().WithMessage("Id is required")
          .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid user id");

        RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required")
          .Must(x => x >= 0 && (int)x <= 8).WithMessage("Enter a status");
    }

}
