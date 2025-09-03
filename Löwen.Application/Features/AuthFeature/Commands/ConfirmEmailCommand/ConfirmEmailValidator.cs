namespace Löwen.Application.Features.AuthFeature.Commands.ConfirmEmailCommand;

public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailCommand>
{
    public ConfirmEmailValidator()
    {
        RuleFor(x => x.userId)
            .NotEmpty().WithMessage("userId is required")
            .Must(userId => Guid.TryParse(userId, out _))
            .WithMessage("userId must be a valid GUID");
        RuleFor(x => x.Token).NotEmpty().WithMessage("Token is required");
    }
}
