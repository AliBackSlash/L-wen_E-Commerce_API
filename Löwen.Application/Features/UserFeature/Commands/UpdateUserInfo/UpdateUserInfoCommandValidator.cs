namespace Löwen.Application.Features.UserFeature.Commands.UpdateUserInfoCommand;

public class UpdateUserInfoCommandValidator : AbstractValidator<UpdateUserInfoCommand>
{
    public UpdateUserInfoCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required")
          .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid id");

        RuleFor(x => x.fName)
            .Must( fn => (fn.Length > 2 && fn.Length < 50)).WithMessage("F-Name must be between 3 and 50 characters.");

        RuleFor(x => x.mName).Must(mn => mn is null || ((mn.Length > 2 && mn.Length < 50) )).WithMessage("M-Name must be between 3 and 50 characters.");

        RuleFor(x => x.lName)
            .Must(ln => (ln.Length > 2 && ln.Length < 50)).WithMessage("L-Name must be between 3 and 50 characters.");

        RuleFor(x => x.PhoneNumber)
            .Must(ph => (ph.Length is 11)).MaximumLength(11).WithMessage("phoneNumber must be 11 numbers");

    }
}
