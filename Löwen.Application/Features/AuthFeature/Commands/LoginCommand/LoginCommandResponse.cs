namespace Löwen.Application.Features.AuthFeature.Commands.LoginCommand;

public class LoginCommandResponse
{
    public required string Token { get; set; }
    public required string Email { get; set; }
    public required string UserName { get; set; }
    public required List<string> Roles { get; set; }
    public required DateTime Expiration { get; set; }
}