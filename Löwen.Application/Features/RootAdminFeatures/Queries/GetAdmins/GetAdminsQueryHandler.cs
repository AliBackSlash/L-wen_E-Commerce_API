using Löwen.Application.Features.RootAdminFeatures.Queries.GetAdminById;
using Löwen.Domain.Abstractions.IServices.IAppUserServices;

namespace Löwen.Application.Features.RootAdminFeatures.Queries.GetAdmins;

public class GetAdminsQueryHandler(IAppUserService userService) : IQueryHandler<GetAdminsQuery, List<GetUsersQueryResponse>>
{
    public async Task<Result<List<GetUsersQueryResponse>>> Handle(GetAdminsQuery command, CancellationToken ct)
    {
        var GetResult = await userService.GetAllAsync(command.Role);
        if (GetResult.IsFailure)
            return Result.Failure<List<GetUsersQueryResponse>>(GetResult.Errors);

        return Result.Success(GetUsersQueryResponse.Map(GetResult.Value));
    }
}
