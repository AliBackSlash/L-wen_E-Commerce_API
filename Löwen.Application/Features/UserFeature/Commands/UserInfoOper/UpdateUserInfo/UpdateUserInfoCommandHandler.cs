namespace Löwen.Application.Features.UserFeature.Commands.UserInfoOper.UpdateUserInfo;

internal class UpdateUserInfoCommandHandler(IAppUserService userService) : ICommandHandler<UpdateUserInfoCommand, UpdateUserInfoCommandResponse>
{
    public async Task<Result<UpdateUserInfoCommandResponse>> Handle(UpdateUserInfoCommand command, CancellationToken ct)
    {
        var createResult = await userService.UpdateUserInfoAsync
            (new(command.Id, command.fName,command.mName,command.lName,command.DateOfBirth,command.PhoneNumber, command.Gender,command.AddressDetails));

        if (createResult.IsFailure)
            return Result.Failure<UpdateUserInfoCommandResponse>(createResult.Errors);

        return Result.Success(new UpdateUserInfoCommandResponse
        {
            token = createResult.Value.token
        });
        
    }
}
