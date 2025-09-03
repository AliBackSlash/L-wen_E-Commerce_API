namespace Löwen.Application.Features.SendEmailFeature.SendResetTokenCommand;

internal class SendResetTokenCommandValidator : AbstractValidator<SendResetTokenCommand>
{
    public SendResetTokenCommandValidator()
    {
        RuleFor(x => x.email).EmailAddress().NotEmpty().WithMessage("Valid Email Is Required");

    }
}
