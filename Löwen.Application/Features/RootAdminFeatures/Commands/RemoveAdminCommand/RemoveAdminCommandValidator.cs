namespace Löwen.Application.Features.RootAdminFeatures.Commands.RemoveAdminCommand;

public class RemoveAdminCommandValidator : AbstractValidator<RemoveAdminCommand>
{
    public RemoveAdminCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required")
            .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid id");
    }
}