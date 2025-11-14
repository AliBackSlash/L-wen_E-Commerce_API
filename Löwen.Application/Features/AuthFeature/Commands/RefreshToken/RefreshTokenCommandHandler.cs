

using Löwen.Application.Messaging.ICommand;
using Löwen.Domain.Abstractions.IServices.IAppUserServices;

namespace Löwen.Application.Features.AuthFeature.Commands.RefreshToken;

public class RefreshTokenCommandHandler(IAppUserService userService) : ICommandHandler<RefreshTokenCommand, RefreshTokenCommandResponse>
{
    public async Task<Result<RefreshTokenCommandResponse>> Handle(RefreshTokenCommand command, CancellationToken ct)
    {
        var result = await userService.RefreshTokenAsync(command.refreshToken, ct);

        if (result.IsFailure)
            return Result.Failure<RefreshTokenCommandResponse>(result.Errors);
            
        return Result.Success(new RefreshTokenCommandResponse 
        { 
            accessToken = result.Value.accessToken
        });

    }
}
