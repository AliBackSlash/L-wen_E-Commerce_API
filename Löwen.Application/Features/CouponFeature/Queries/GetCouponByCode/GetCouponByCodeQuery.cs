using Löwen.Application.Messaging.IQuery;
using Löwen.Domain.Layer_Dtos.Cart;
using Löwen.Domain.Layer_Dtos.Order;

namespace Löwen.Application.Features.CouponFeature.Queries.GetCouponByCode;

public record GetCouponByCodeQuery(string Code) : IQuery<GetCouponQueryResponse>;
