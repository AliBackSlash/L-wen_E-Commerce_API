namespace Löwen.Application.Features.UploadFeature.UpdateProfileImageCommand;

public class UpdateProfileImageCommandValidator : AbstractValidator<UpdateProfileImageCommand>
{
    public UpdateProfileImageCommandValidator()
    {
        RuleFor(x => x.userId)
        .NotEmpty()
        .Must(id => Guid.TryParse(id, out _))
        .WithMessage("Id must be a valid GUID");

        RuleFor(x => x.ProfileImagePath).NotEmpty().WithMessage("Profile Image Path is required");
    }
}
