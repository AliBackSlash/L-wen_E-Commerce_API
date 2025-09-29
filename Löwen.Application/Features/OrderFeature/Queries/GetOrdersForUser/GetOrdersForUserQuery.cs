using Löwen.Application.Features.OrderFeature.Queries.OrderDetailsResponse;

namespace Löwen.Application.Features.OrderFeature.Queries.GetOrdersForUser;

public record GetOrdersForUserQuery(string userId, int PageNumber, byte PageSize) : IQuery<PagedResult<GetOrderDetailsQueryResponse>>;
