using Löwen.Application.Messaging.IQuery;

namespace Löwen.Application.Features.ProductFeature.Queries.GetAllProductPaged;

public record GetAllProductPagedQuery(int PageNumber,byte PageSize) : IQuery<PagedResult<GetProductQueryResponse>>;
