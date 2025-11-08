using Löwen.Application.Messaging.ICommand;
using Löwen.Domain.Abstractions.IServices.IAppUserServices;

namespace Löwen.Application.Features.AuthFeature.Commands.ConfirmEmailCommand;

public class ConfirmEmailHandler(IAppUserService userService) : ICommandHandler<ConfirmEmailCommand, ConfirmEmailResponse>
{
    public async Task<Result<ConfirmEmailResponse>> Handle(ConfirmEmailCommand command, CancellationToken ct)
    {
        var result = await userService.ConfirmEmailAsync(command.userId, command.Token);

        if (result.IsSuccess)
            return Result.Success(new ConfirmEmailResponse { Token = result.Value });

        return Result.Failure<ConfirmEmailResponse>(result.Errors);
    }
}
