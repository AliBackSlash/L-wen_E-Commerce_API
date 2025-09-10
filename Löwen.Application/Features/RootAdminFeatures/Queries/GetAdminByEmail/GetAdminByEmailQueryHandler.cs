using Löwen.Application.Features.RootAdminFeatures.Queries.GetAdminById;

namespace Löwen.Application.Features.RootAdminFeatures.Queries.GetAdminByEmail;

public class GetAdminByEmailQueryHandler(IAppUserService userService) : IQueryHandler<GetAdminByEmailQuery, GetUserQueryResponse>
{
    public async Task<Result<GetUserQueryResponse>> Handle(GetAdminByEmailQuery command, CancellationToken cancellationToken)
    {
        var GetResult = await userService.GetUserByEmailAsync(command.email,command.Role);
        if (GetResult.IsFailure)
            return Result.Failure<GetUserQueryResponse>(GetResult.Errors);

        return Result.Success(GetUserQueryResponse.Map(GetResult.Value));
    }
}
