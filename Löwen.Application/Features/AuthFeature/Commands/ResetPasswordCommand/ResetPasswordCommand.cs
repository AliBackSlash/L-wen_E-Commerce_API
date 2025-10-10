namespace Löwen.Application.Features.AuthFeature.Commands.ResetPasswordCommand;

public record ResetPasswordCommand(string Email,string Password) : ICommand;

