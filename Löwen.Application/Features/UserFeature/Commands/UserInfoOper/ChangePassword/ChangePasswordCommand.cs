namespace Löwen.Application.Features.UserFeature.Commands.UserInfoOper.ChangePassword;

public record ChangePasswordCommand(string Id,string currentPassword,string newPassword, string ConfermPassword) : ICommand<ChangePasswordCommandResponse>;
