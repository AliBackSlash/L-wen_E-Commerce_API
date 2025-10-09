using Löwen.Domain.Abstractions.IServices.IEntitiesServices;

namespace Löwen.Application.Features.UserFeature.Commands.DeleteUserImage;

public class DeleteUserImageCommandHandler(IAppUserService UserService)
    : ICommandHandler<DeleteUserImageCommand,string>
{
    public async Task<Result<string>> Handle(DeleteUserImageCommand command, CancellationToken ct)
    {
        var deleteResult = await UserService.RemoveUserImageAsync(command.uId);

        if (deleteResult.IsFailure)
            return Result.Failure<string>(deleteResult.Errors);

        return deleteResult;
    }
}
