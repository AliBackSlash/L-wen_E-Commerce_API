using Löwen.Application.Features.OrderFeature.Queries.OrderDetailsResponse;

namespace Löwen.Application.Features.UserFeature.Queries.GetAllOrders;

public record GetAllOrdersQuery(int PageNumber, byte PageSize) : IQuery<PagedResult<GetOrderDetailsQueryResponse>>;
