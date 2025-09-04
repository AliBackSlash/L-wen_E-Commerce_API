using Löwen.Application.Abstractions.IServices.IdentityServices;

namespace Löwen.Application.Features.UserFeature.Commands.ChangePasswordCommand;

internal class ChangePasswordCommandHandler(IAppUserService userService) : ICommandHandler<ChangePasswordCommand, ChangePasswordCommandResponse>
{
    public async Task<Result<ChangePasswordCommandResponse>> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        var id = userService.GetUserIdFromToken(command.token);
        if(id.IsFailure)
            return Result.Failure<ChangePasswordCommandResponse>(id.Errors);

        var changeResult = await userService.ChangePasswordAsync(command.token, command.currentPassword, command.newPassword);
        if (changeResult.IsFailure)
            return Result.Failure<ChangePasswordCommandResponse>(changeResult.Errors);

        return Result.Success(new ChangePasswordCommandResponse { token = changeResult.Value });
    }
}
