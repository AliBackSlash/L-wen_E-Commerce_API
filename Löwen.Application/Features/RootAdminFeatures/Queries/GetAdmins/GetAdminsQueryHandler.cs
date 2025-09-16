using Löwen.Application.Features.RootAdminFeatures.Queries.GetAdminById;
using Löwen.Domain.Abstractions.IServices.IAppUserServices;

namespace Löwen.Application.Features.RootAdminFeatures.Queries.GetAdmins;

public class GetAdminsQueryHandler(IAppUserService userService) : IQueryHandler<GetAdminsQuery, List<GetdminsQueryResponse>>
{
    public async Task<Result<List<GetdminsQueryResponse>>> Handle(GetAdminsQuery command, CancellationToken ct)
    {
        var GetResult = await userService.GetAllAsync(command.Role);
        if (GetResult.IsFailure)
            return Result.Failure<List<GetdminsQueryResponse>>(GetResult.Errors);

        return Result.Success(GetdminsQueryResponse.Map(GetResult.Value));
    }
}
