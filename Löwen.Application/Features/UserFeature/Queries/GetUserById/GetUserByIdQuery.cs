using Löwen.Application.Messaging.ICache;
using Löwen.Application.Messaging.ICommand;
using Löwen.Application.Messaging.IQuery;

namespace Löwen.Application.Features.UserFeature.Queries.GetUserById;

public record GetUserByIdQuery(string Id) : IQueryWithCache<GetUserByIdQueryResponse>
{
    public string CacheKey => $"get-user-by-id/{Id}";

    public string Prefix => PrefexesAndDurationsForCacheSettings.User_prefix;

    public int DurationMinutes => PrefexesAndDurationsForCacheSettings.User_durationMinutes;
}

