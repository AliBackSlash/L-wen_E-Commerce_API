using Löwen.Domain.Abstractions.IServices.IAppUserServices;

namespace Löwen.Application.Features.RootAdminFeatures.Queries.GetAdminById;

public class GetAdminByIdQueryHandler(IAppUserService userService) : IQueryHandler<GetAdminByIdQuery, GetUserQueryResponse>
{
    public async Task<Result<GetUserQueryResponse>> Handle(GetAdminByIdQuery command, CancellationToken ct)
    {
        var GetResult = await userService.GetUserByIdAsync(command.Id.ToString(),command.Role);
        if (GetResult.IsFailure)
            return Result.Failure<GetUserQueryResponse>(GetResult.Errors);

        return Result.Success(GetUserQueryResponse.Map(GetResult.Value));
    }
}
