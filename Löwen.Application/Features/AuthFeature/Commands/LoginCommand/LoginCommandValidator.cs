namespace Löwen.Application.Features.AuthFeature.Commands.LoginCommand;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.UserNameOrEmail).NotEmpty().WithMessage("username or email is required");
    }
}
