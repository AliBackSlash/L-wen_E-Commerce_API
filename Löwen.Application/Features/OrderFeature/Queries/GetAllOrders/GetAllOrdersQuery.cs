using Löwen.Application.Features.OrderFeature.Queries.OrderDetailsResponse;
using Löwen.Application.Messaging.IQuery;

namespace Löwen.Application.Features.OrderFeature.Queries.GetAllOrders;

public record GetAllOrdersQuery(int PageNumber, byte PageSize) : IQuery<PagedResult<GetOrderDetailsQueryResponse>>;
