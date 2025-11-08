using Löwen.Application.Messaging.ICache;
using Löwen.Application.Messaging.IQuery;

namespace Löwen.Application.Features.CartFeature.Queries.GetCartByUser;

public record GetCartByUserQuery(string userId, int PageNumber, byte PageSize) : IQueryWithCache<PagedResult<GetCartByUserQueryresponse>>
{
    public string CacheKey => $"get-cart-by-user/{userId},{PageNumber},{PageSize}";

    public string Prefix => PrefexesAndDurationsForCacheSettings.User_prefix;

    public int DurationMinutes => PrefexesAndDurationsForCacheSettings.User_durationMinutes;
}
