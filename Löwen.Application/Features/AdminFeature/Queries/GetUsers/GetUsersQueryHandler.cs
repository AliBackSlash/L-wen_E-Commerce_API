using Löwen.Domain.ConfigurationClasses.Pagination;
using Löwen.Domain.Pagination;
using Microsoft.Extensions.Options;

namespace Löwen.Application.Features.AdminFeature.Queries.GetUsers;

public class GetUsersQueryHandler(IAppUserService userService,IOptions<PaginationSettings> PSettings ) : ICommandHandler<GetUsersQuery, PagedResult<GetUsersQueryResponse>>
{
    public async Task<Result<PagedResult<GetUsersQueryResponse>>> Handle(GetUsersQuery command, CancellationToken ct)
    {
        var users = await userService.GetAllAsync(new PaginationParams
        {
            maxPageSize = PSettings.Value.maxPageSize,
            PageNumber = command.PageNumber,
            Take = command.PageSize
        });
        if (users.IsFailure)
            return Result.Failure<PagedResult<GetUsersQueryResponse>> (users.Errors);

        return Result.Success(PagedResult<GetUsersQueryResponse>.Create(GetUsersQueryResponse.Map(users.Value.Items),users.Value.TotalCount,
            users.Value.PageNumber,users.Value.PageSize));
    }
}
