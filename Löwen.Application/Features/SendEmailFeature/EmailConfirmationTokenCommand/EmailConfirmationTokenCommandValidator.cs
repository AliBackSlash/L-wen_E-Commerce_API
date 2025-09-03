namespace Löwen.Application.Features.SendEmailFeature.EmailConfirmationTokenCommand;

internal class EmailConfirmationTokenCommandValidator : AbstractValidator<EmailConfirmationTokenCommand>
{
    public EmailConfirmationTokenCommandValidator()
    {
        RuleFor(x => x.email).EmailAddress().NotEmpty().WithMessage("Valid Email Is Required");

    }
}
