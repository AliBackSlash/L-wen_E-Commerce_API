using Löwen.Application.Features.AdminFeature.Queries.GetProducts;
using Löwen.Application.Messaging.ICache;
using Löwen.Application.Messaging.ICommand;
using Löwen.Application.Messaging.IQuery;
using Löwen.Domain.Pagination;

namespace Löwen.Application.Features.AdminFeature.Queries.GetProductsByIdOrName;

public record GetProductsByIdOrNameQuery(string IdOrName,int PageNumber,byte PageSize) : IQueryWithCache<PagedResult<GetProductsQueryResponse>>
{
    public string CacheKey => $"get-products-paged-filter-by-id-or-name/{IdOrName},{PageNumber},{PageSize}";

    public string Prefix => PrefexesAndDurationsForCacheSettings.Product_prefix;

    public int DurationMinutes => PrefexesAndDurationsForCacheSettings.Product_ForAdmins_durationMinutes;

}

