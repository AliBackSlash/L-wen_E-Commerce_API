using Löwen.Domain.Abstractions.IServices.IAppUserServices;

namespace Löwen.Application.Features.UserFeature.Commands.UserInfoOper.ChangePassword;

internal class ChangePasswordCommandHandler(IAppUserService userService) : ICommandHandler<ChangePasswordCommand, ChangePasswordCommandResponse>
{
    public async Task<Result<ChangePasswordCommandResponse>> Handle(ChangePasswordCommand command, CancellationToken ct)
    {       
        var changeResult = await userService.ChangePasswordAsync(command.Id, command.currentPassword, command.newPassword);
        if (changeResult.IsFailure)
            return Result.Failure<ChangePasswordCommandResponse>(changeResult.Errors);

        return Result.Success(new ChangePasswordCommandResponse { token = changeResult.Value });
    }
}
