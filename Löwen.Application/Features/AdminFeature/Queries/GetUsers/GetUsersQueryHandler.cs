using Löwen.Application.Messaging.ICommand;
using Löwen.Application.Messaging.IQuery;
using Löwen.Domain.ConfigurationClasses.Pagination;
using Löwen.Domain.Pagination;
using Microsoft.Extensions.Options;

namespace Löwen.Application.Features.AdminFeature.Queries.GetUsers;

public class GetUsersQueryHandler(IAppUserService userService,IOptions<PaginationSettings> PSettings ) : IQueryWithCacheHandler<GetUsersQuery, PagedResult<GetUsersQueryResponse>>
{
    public async Task<Result<PagedResult<GetUsersQueryResponse>>> Handle(GetUsersQuery query, CancellationToken ct)
    {
        var users = await userService.GetAllAsync(new PaginationParams
        {
            maxPageSize = PSettings.Value.maxPageSize,
            PageNumber = query.PageNumber,
            Take = query.PageSize
        },ct);
        if (users.IsFailure)
            return Result.Failure<PagedResult<GetUsersQueryResponse>> (users.Errors);

        return Result.Success(PagedResult<GetUsersQueryResponse>.Create(users.Value.Items.Select(x=> new GetUsersQueryResponse
        {
            Id = x.Id,
            UserName = x.UserName,
            Name = x.FName + (string.IsNullOrWhiteSpace(x.MName) ? " " : $" {x.MName} ") + x.LName,
            PhoneNumber = x.PhoneNumber,
            Gender = x.Gender,
            IsActive = x.IsActive,
        }),users.Value.TotalCount,
            users.Value.PageNumber,users.Value.PageSize));
    }
}
