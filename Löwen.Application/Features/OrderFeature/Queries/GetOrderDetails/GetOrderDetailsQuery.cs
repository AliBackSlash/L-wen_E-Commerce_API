namespace Löwen.Application.Features.UserFeature.Queries.GetOrderDetails;

public record GetOrderDetailsQuery(string orderId) : IQuery<GetOrderDetailsQueryResponse>;
