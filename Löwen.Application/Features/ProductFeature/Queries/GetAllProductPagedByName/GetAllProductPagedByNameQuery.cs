using Löwen.Application.Messaging.IQuery;

namespace Löwen.Application.Features.ProductFeature.Queries.GetAllProductPagedByName;

public record GetAllProductPagedByNameQuery(string Name,int PageNumber,byte PageSize) : IQuery<PagedResult<GetProductQueryResponse>>;
