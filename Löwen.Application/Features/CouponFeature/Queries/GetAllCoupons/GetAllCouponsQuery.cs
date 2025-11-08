using Löwen.Application.Messaging.IQuery;
using Löwen.Domain.Layer_Dtos.Cart;
using Löwen.Domain.Layer_Dtos.Order;

namespace Löwen.Application.Features.CouponFeature.Queries.GetAllCoupons;

public record GetAllCouponsQuery(int PageNumber, byte PageSize) : IQuery<PagedResult<GetCouponQueryResponse>>;
