
using Löwen.Application.Features.UserFeature.Commands.ChangePasswordCommand;
using Löwen.Domain.Abstractions.IServices.IAppUserServices;
using Löwen.Domain.Entities;
using System.Xml.Linq;

namespace Löwen.Application.Features.UserFeature.Commands.UpdateUserInfoCommand;

internal class UpdateUserInfoCommandHandler(IAppUserService userService) : ICommandHandler<UpdateUserInfoCommand, UpdateUserInfoCommandResponse>
{
    public async Task<Result<UpdateUserInfoCommandResponse>> Handle(UpdateUserInfoCommand command, CancellationToken ct)
    {
        var createResult = await userService.UpdateUserInfoAsync
            (new(command.Id, command.fName,command.mName,command.lName,command.DateOfBirth,command.PhoneNumber, command.Gender));

        if (createResult.IsFailure)
            return Result.Failure<UpdateUserInfoCommandResponse>(createResult.Errors);

        return Result.Success(new UpdateUserInfoCommandResponse
        {
            token = createResult.Value.token
        });
        
    }
}
