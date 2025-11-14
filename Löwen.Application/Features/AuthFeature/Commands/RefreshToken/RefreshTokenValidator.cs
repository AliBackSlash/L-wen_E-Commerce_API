namespace Löwen.Application.Features.AuthFeature.Commands.RefreshToken;

public class RefreshTokenValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenValidator()
    {
        RuleFor(x => x.refreshToken).NotEmpty().WithMessage("refreshToken is required");
    }
}
