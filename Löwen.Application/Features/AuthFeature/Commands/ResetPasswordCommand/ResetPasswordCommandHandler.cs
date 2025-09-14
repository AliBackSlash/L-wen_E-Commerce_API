

using Löwen.Domain.Abstractions.IServices.IAppUserServices;

namespace Löwen.Application.Features.AuthFeature.Commands.ResetPasswordCommand;

public class ResetPasswordCommandHandler(IAppUserService userService) : ICommandHandler<ResetPasswordCommand, ResetPasswordCommandResponse>
{
    public async Task<Result<ResetPasswordCommandResponse>> Handle(ResetPasswordCommand command, CancellationToken ct)
    {
        var ResetResult = await userService.ResetPasswordAsync(command.Email, command.token, command.Password);
        if (ResetResult.IsFailure)
            return Result.Failure<ResetPasswordCommandResponse>(ResetResult.Errors);

        return Result.Success(new ResetPasswordCommandResponse {token  = ResetResult.Value });
    }
}
