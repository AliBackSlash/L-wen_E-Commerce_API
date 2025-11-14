

using Löwen.Application.Messaging.ICommand;
using Löwen.Domain.Abstractions.IServices.IAppUserServices;

namespace Löwen.Application.Features.AuthFeature.Commands.LoginCommand;

public class LoginCommandHandler(IAppUserService userService) : ICommandHandler<LoginCommand, LoginCommandResponse>
{
    public async Task<Result<LoginCommandResponse>> Handle(LoginCommand command, CancellationToken ct)
    {
        var result = await userService.LoginAsync(new LoginDto(command.UserNameOrEmail, command.Password), ct);

        if (result.IsFailure)
            return Result.Failure<LoginCommandResponse>(result.Errors);
            
        
        
        return Result.Success(new LoginCommandResponse 
        { 
            accessToken = result.Value.accessToken,
            refreshToken = result.Value.refreshToken
                
        });

    }
}
