namespace Löwen.Application.Features.UserFeature.Commands.ChangePasswordCommand;

public record ChangePasswordCommand(string token,string currentPassword,string newPassword, string ConfermPassword) : ICommand<ChangePasswordCommandResponse>;
