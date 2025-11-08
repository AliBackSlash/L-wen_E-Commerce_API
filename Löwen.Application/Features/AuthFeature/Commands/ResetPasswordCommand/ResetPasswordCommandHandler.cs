

using Löwen.Application.Messaging.ICommand;
using Löwen.Domain.Abstractions.IServices.IAppUserServices;

namespace Löwen.Application.Features.AuthFeature.Commands.ResetPasswordCommand;

public class ResetPasswordCommandHandler(IAppUserService userService) : ICommandHandler<ResetPasswordCommand>
{
    public async Task<Result> Handle(ResetPasswordCommand command, CancellationToken ct)
    {
        var R_P_token = await userService.GenerateRestPasswordTokenAsync(command.Email);
        if(R_P_token.IsFailure)
            return Result.Failure(R_P_token.Errors);

        return await userService.ResetPasswordAsync(command.Email, R_P_token.Value, command.Password);
  

    }
}
