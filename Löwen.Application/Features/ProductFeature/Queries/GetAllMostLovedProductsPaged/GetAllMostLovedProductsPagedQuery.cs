using Löwen.Application.Messaging.IQuery;

namespace Löwen.Application.Features.ProductFeature.Queries.GetAllMostLovedProductsPaged;

public record GetAllMostLovedProductsPagedQuery(int PageNumber,byte PageSize) : IQuery<PagedResult<GetProductQueryResponse>>;
