namespace Löwen.Application.Features.AuthFeature.Commands.ResetPasswordCommand;

public record ResetPasswordCommand(string Email,string token,string Password,string ConfermPassword) : ICommand<ResetPasswordCommandResponse>;

