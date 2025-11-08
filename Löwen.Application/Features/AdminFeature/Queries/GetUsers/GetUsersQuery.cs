using Löwen.Application.Messaging.ICache;
using Löwen.Application.Messaging.ICommand;
using Löwen.Application.Messaging.IQuery;
using Löwen.Domain.Pagination;

namespace Löwen.Application.Features.AdminFeature.Queries.GetUsers;

public record GetUsersQuery(int PageNumber,byte PageSize) : IQueryWithCache<PagedResult<GetUsersQueryResponse>>
{
    public string CacheKey => $"get-users-paged/{PageNumber},{PageSize}";

    public string Prefix => PrefexesAndDurationsForCacheSettings.Users_ForAdmins_prefix;

    public int DurationMinutes => PrefexesAndDurationsForCacheSettings.Users_list_durationMinutes;

}

