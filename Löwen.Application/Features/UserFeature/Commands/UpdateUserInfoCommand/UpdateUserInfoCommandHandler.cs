using Löwen.Application.Abstractions.IServices.IdentityServices;
using Löwen.Application.Features.UserFeature.Commands.ChangePasswordCommand;
using Löwen.Domain.Entities;
using System.Xml.Linq;

namespace Löwen.Application.Features.UserFeature.Commands.UpdateUserInfoCommand;

internal class UpdateUserInfoCommandHandler(IAppUserService userService) : ICommandHandler<UpdateUserInfoCommand, UpdateUserInfoCommandResponse>
{
    public async Task<Result<UpdateUserInfoCommandResponse>> Handle(UpdateUserInfoCommand command, CancellationToken cancellationToken)
    {
        var id = userService.GetUserIdFromToken(command.token);
        if (id.IsFailure)
            return Result.Failure<UpdateUserInfoCommandResponse>(id.Errors);

        var createResult = await userService.UpdateUserInfo
            (new(id.Value,command.fName,command.mName,command.lName,command.phoneNumber));

        if (createResult.IsFailure)
            return Result.Failure<UpdateUserInfoCommandResponse>(createResult.Errors);

        return Result.Success(new UpdateUserInfoCommandResponse
        {
            token = createResult.Value.token
        });
        
    }
}
