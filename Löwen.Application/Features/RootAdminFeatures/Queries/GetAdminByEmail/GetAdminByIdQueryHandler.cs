namespace Löwen.Application.Features.RootAdminFeatures.Queries.GetAdminByEmail;

public class GetAdminByIdQueryHandler(IAppUserService userService) : IQueryHandler<GetAdminByIdQuery, GetAdminByIdQueryResponse>
{
    public async Task<Result<GetAdminByIdQueryResponse>> Handle(GetAdminByIdQuery command, CancellationToken cancellationToken)
    {
        var GetResult = await userService.GetUserById(command.Id.ToString(),command.Role);
        if (GetResult.IsFailure)
            return Result.Failure<GetAdminByIdQueryResponse>(GetResult.Errors);

        return Result.Success(GetAdminByIdQueryResponse.Map(GetResult.Value));
    }
}
