namespace Löwen.Application.Features.UserFeature.Queries.GetUserByEmail;

public class GetUserByEmailQueryHandler(IAppUserService userService) : ICommandHandler<GetUserByEmailQuery, GetUserByEmailQueryResponse>
{
    public async Task<Result<GetUserByEmailQueryResponse>> Handle(GetUserByEmailQuery command, CancellationToken ct)
    {
        var user = await userService.GetUserByEmailAsync(command.email);
        if (user.IsFailure)
            return Result.Failure<GetUserByEmailQueryResponse>(user.Errors);

        return Result.Success(new GetUserByEmailQueryResponse
        {
            FName = user.Value.FName,
            MName = user.Value.MName,
            LName = user.Value.LName,
            PhoneNumber = user.Value.PhoneNumber,
            ImagePath = user.Value.ImagePath
        });
    }
}
