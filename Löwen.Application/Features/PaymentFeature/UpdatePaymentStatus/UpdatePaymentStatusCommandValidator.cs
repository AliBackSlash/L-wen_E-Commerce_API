namespace Löwen.Application.Features.PaymentFeature.UpdatePaymentStatus;

public class UpdatePaymentStatusCommandValidator : AbstractValidator<UpdatePaymentStatusCommand>
{
    public UpdatePaymentStatusCommandValidator()
    {
        RuleFor(x => x.orderId)
         .NotEmpty()
         .WithMessage("OrderId is required.")
         .Must(id => Guid.TryParse(id, out _))
         .WithMessage("OrderId must be a valid Guid.");

        RuleFor(x => x.status)
            .IsInEnum()
            .WithMessage("PaymentStatus is invalid.");

    }

}
