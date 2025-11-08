using Löwen.Application.Features.OrderFeature.Queries.OrderDetailsResponse;
using Löwen.Application.Messaging.IQuery;

namespace Löwen.Application.Features.OrderFeature.Queries.GetOrderDetails;

public record GetOrderDetailsQuery(string orderId) : IQuery<GetOrderDetailsQueryResponse>;
