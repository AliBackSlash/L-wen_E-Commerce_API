using Löwen.Application.Messaging.ICache;
using Löwen.Application.Messaging.ICommand;
using Löwen.Application.Messaging.IQuery;

namespace Löwen.Application.Features.UserFeature.Queries.GetUserByEmail;

public record GetUserByEmailQuery(string email) : IQueryWithCache<GetUserByEmailQueryResponse>
{
    public string CacheKey =>  $"get-user-by-email/{email}";

    public string Prefix => PrefexesAndDurationsForCacheSettings.User_prefix;

    public int DurationMinutes => PrefexesAndDurationsForCacheSettings.User_durationMinutes;
}

