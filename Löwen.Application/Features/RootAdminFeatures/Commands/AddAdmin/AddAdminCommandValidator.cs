namespace Löwen.Application.Features.RootAdminFeatures.Commands.AddAdmin;

public class AddAdminCommandValidator : AbstractValidator<AddAdminCommand>
{
    public AddAdminCommandValidator()
    {
        RuleFor(x => x.Email).EmailAddress().NotEmpty().WithMessage("Valid Email Is Required");
        RuleFor(x => x.UserName).NotEmpty().MinimumLength(6).MaximumLength(16).WithMessage("UserName must be between 6-16 characters");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"\d").WithMessage("Password must contain at least one number.")
            .Matches(@"[\W_]").WithMessage("Password must contain at least one special character.");

        RuleFor(x => x.FName)
            .Length(3, 50).WithMessage("F-Name must be between 3 and 50 characters.");

        RuleFor(x => x.MName).Length(3, 50).WithMessage("M-Name must be between 3 and 50 characters.");

        RuleFor(x => x.LName)
            .Length(3, 50)
            .WithMessage("L-Name must be between 3 and 50 characters.");

        RuleFor(x => x.PhoneNumber)
             .Length(1, 11).WithMessage("phoneNumber must be 11 numbers")
             .Matches(@"\d").WithMessage("phoneNumber must be numaric");
       
        //###
        RuleFor(x => x.DateOfBirth)
            .Must(dob => dob <= DateOnly.FromDateTime(DateTime.Now.AddYears(-16)))
            .WithMessage("Admin age must be at least 16 years old.");
    }
}
