namespace Löwen.Application.Features.PaymentFeature.AddPayment;
public class AddPaymentCommandValidator : AbstractValidator<AddPaymentCommand>
{
  public AddPaymentCommandValidator()
  {
    RuleFor(x => x.OrderId)
        .NotEmpty()
        .WithMessage("OrderId is required.")
        .Must(id => Guid.TryParse(id, out _))
        .WithMessage("OrderId must be a valid Guid.");

    RuleFor(x => x.Amount)
        .GreaterThan(0)
        .WithMessage("Amount must be greater than zero.");

    RuleFor(x => x.PaymentMethod)
        .IsInEnum()
        .WithMessage("PaymentMethod is invalid.");

    RuleFor(x => x.TransactionId)
        .MaximumLength(100)
        .WithMessage("TransactionId must not exceed 100 characters.");

    RuleFor(x => x.Status)
        .IsInEnum()
        .WithMessage("PaymentStatus is invalid.");
    }
}
