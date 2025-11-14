namespace Löwen.Application.Features.AuthFeature.Commands.LoginCommand;

public class LoginCommandResponse
{
    public required string  accessToken { get; set; }
    public required string  refreshToken { get; set; }

}