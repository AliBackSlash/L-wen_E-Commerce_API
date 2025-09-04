namespace Löwen.Application.Features.UserFeature.Commands.UpdateUserInfoCommand;

public class UpdateUserInfoCommandValidator : AbstractValidator<UpdateUserInfoCommand>
{
    public UpdateUserInfoCommandValidator()
    {
        RuleFor(x => x.token)
            .NotEmpty().WithMessage("token is required");
           
        RuleFor(x => x.fName)
/*            .MinimumLength(2).WithMessage("Name must be at least 2 characters long.")
*/            .MaximumLength(50).WithMessage("F-Name cannot exceed 50 characters.");

        RuleFor(x => x.mName).MaximumLength(50).WithMessage("M-Name cannot exceed 50 characters.");

        RuleFor(x => x.lName)
/*            .MinimumLength(2).WithMessage("Name must be at least 2 characters long.")
*/            .MaximumLength(50).WithMessage("L-Name cannot exceed 50 characters.");
        
        RuleFor(x => x.phoneNumber)
/*            .MinimumLength(11)
*/            .MaximumLength(11).WithMessage("phoneNumber must be 11 numbers");

    }
}
