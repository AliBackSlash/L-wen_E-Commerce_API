namespace Löwen.Application.Features.UserFeature.Commands.DeleteUserImage;

public class DeleteUserImageCommandValidator : AbstractValidator<DeleteUserImageCommand>
{
    public DeleteUserImageCommandValidator()
    {
        RuleFor(x => x.uId).NotEmpty().WithMessage("Id is required")
            .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid id");
    }
}