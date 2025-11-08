using Löwen.Application.Messaging.ICache;
using Löwen.Application.Messaging.ICommand;
using Löwen.Application.Messaging.IQuery;
using Löwen.Domain.Pagination;

namespace Löwen.Application.Features.AdminFeature.Queries.GetProducts;

public record GetProductsQuery(int PageNumber, byte PageSize) : IQueryWithCache<PagedResult<GetProductsQueryResponse>>
{
    public string CacheKey => $"get-products-paged/{PageNumber},{PageSize}";

    public string Prefix => PrefexesAndDurationsForCacheSettings.Product_prefix;

    public int DurationMinutes => PrefexesAndDurationsForCacheSettings.Product_ForAdmins_durationMinutes;
}
;

