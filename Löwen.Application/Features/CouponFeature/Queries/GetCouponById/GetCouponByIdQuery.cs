using Löwen.Domain.Layer_Dtos.Cart;
using Löwen.Domain.Layer_Dtos.Order;

namespace Löwen.Application.Features.CouponFeature.Queries.GetCouponById;

public record GetCouponByIdQuery(string CouponId) : IQuery<GetCouponQueryResponse>;
