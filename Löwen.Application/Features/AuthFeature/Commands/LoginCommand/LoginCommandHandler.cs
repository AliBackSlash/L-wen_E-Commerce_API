using Löwen.Application.Abstractions.IServices.IdentityServices;

namespace Löwen.Application.Features.AuthFeature.Commands.LoginCommand;

public class LoginCommandHandler(IAppUserService userService) : ICommandHandler<LoginCommand, LoginCommandResponse>
{
    public async Task<Result<LoginCommandResponse>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var result = await userService.LoginAsync(new LoginDto(command.UserNameOrEmail, command.Password), cancellationToken);
        
        if(result.IsSuccess)
            return Result.Success(new LoginCommandResponse 
            { 
                Token = result.Value.Token ,
                Email = result.Value.Email,
                UserName = result.Value.UserName,
                Roles = result.Value.Roles,
                Expiration = result.Value.Expiration
               
            });

        return Result.Failure<LoginCommandResponse>(result.Errors);
    }
}
