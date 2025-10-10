namespace Löwen.Application.Features.AuthFeature.Commands.ResetPasswordCommand;

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(x => x.Email)
             .NotEmpty().WithMessage("Email is required.")
             .EmailAddress().WithMessage("Email must be valid.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"\d").WithMessage("Password must contain at least one number.")
            .Matches(@"[\W_]").WithMessage("Password must contain at least one special character.");


    }
}
