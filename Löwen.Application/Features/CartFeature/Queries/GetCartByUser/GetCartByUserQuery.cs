using Löwen.Domain.Layer_Dtos.Cart;
using Löwen.Domain.Layer_Dtos.Order;

namespace Löwen.Application.Features.CartFeature.Queries.GetCartByUser;

public record GetCartByUserQuery(string userId, int PageNumber, byte PageSize) : IQuery<PagedResult<GetCartByUserQueryresponse>>;
