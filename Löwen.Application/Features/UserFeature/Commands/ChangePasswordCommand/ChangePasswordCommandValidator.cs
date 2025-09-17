namespace Löwen.Application.Features.UserFeature.Commands.ChangePasswordCommand;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required")
            .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid id");

        RuleFor(x => x.currentPassword).NotEmpty().WithMessage("Current password is required");
        RuleFor(x => x.newPassword)
                   .NotEmpty().WithMessage("Password is required.")
                   .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                   .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                   .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                   .Matches(@"\d").WithMessage("Password must contain at least one number.")
                   .Matches(@"[\W_]").WithMessage("Password must contain at least one special character.");

        RuleFor(x => x.ConfermPassword)
            .Equal(x => x.newPassword).WithMessage("Confirm Password must match Password.");
    }
}
