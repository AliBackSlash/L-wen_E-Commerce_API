namespace Löwen.Application.Features.UserFeature.Commands.ChangePasswordCommand;

public record ChangePasswordCommand(string Id,string currentPassword,string newPassword, string ConfermPassword) : ICommand<ChangePasswordCommandResponse>;
