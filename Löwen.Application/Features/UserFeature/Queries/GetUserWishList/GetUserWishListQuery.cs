using Löwen.Application.Features.OrderFeature.Queries.OrderDetailsResponse;
using Löwen.Application.Messaging.IQuery;

namespace Löwen.Application.Features.UserFeature.Queries.GetUserWishList;

public record GetUserWishListQuery(string userId, int PageNumber, byte PageSize) : IQuery<PagedResult<GetUserWishListQueryResponse>>;
