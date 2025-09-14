


using Löwen.Application.Features.UserFeature.Commands.ChangePasswordCommand;
using Löwen.Domain.Abstractions.IServices.IAppUserServices;

namespace Löwen.Application.Features.UserFeature.Queries;

public class GetUserByIdQueryHandler(IAppUserService userService) : ICommandHandler<GetUserByIdQuery, GetUserByIdQueryResponse>
{
    public async Task<Result<GetUserByIdQueryResponse>> Handle(GetUserByIdQuery command, CancellationToken ct)
    {
        var id =  userService.GetUserIdFromToken(command.token);

        if (id.IsFailure)
            return Result.Failure<GetUserByIdQueryResponse>(id.Errors);

        var user = await userService.GetUserByIdAsync(id.Value);
        if (user.IsFailure)
            return Result.Failure<GetUserByIdQueryResponse>(user.Errors);

        return Result.Success(new GetUserByIdQueryResponse
        {
            FName = user.Value.FName,
            MName = user.Value.MName,
            LName = user.Value.LName,
            PhoneNumber = user.Value.PhoneNumber,
            ImagePath = user.Value.ImagePath
        });
    }
}
