using Löwen.Application.Features.UserFeature.Commands.ChangePasswordCommand;
using Löwen.Domain.Abstractions.IServices.IAppUserServices;

namespace Löwen.Application.Features.UserFeature.Queries.GetUserById;

public class GetUserByIdQueryHandler(IAppUserService userService) : ICommandHandler<GetUserByIdQuery, GetUserByIdQueryResponse>
{
    public async Task<Result<GetUserByIdQueryResponse>> Handle(GetUserByIdQuery command, CancellationToken ct)
    {
        //admin
        var user = await userService.GetUserByIdAsync(command.Id);
        if (user.IsFailure)
            return Result.Failure<GetUserByIdQueryResponse>(user.Errors);

        return Result.Success(new GetUserByIdQueryResponse
        {
            Name = user.Value.FName + (user.Value.MName is null ? "" : " " + user.Value.MName) + " " +  user.Value.LName,
            UserName = user.Value.UserName,
            Email = user.Value.Email,   
            Gender = user.Value.Gender,
            DateOfBirth = user.Value.DateOfBirth,
            PhoneNumber = user.Value.PhoneNumber,
            ImagePath = user.Value.ImagePath
        });
    }
}
