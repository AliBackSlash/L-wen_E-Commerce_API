using Löwen.Application.Features.OrderFeature.Queries.OrderDetailsResponse;
using Löwen.Application.Messaging.IQuery;
using Löwen.Domain.Abstractions.IServices.IEntitiesServices;

namespace Löwen.Application.Features.OrderFeature.Queries.GetOrderDetails;

internal class GetOrderDetailsQueryHandler(IOrderService orderIService) : IQueryHandler<GetOrderDetailsQuery, GetOrderDetailsQueryResponse>
{
    public async Task<Result<GetOrderDetailsQueryResponse>> Handle(GetOrderDetailsQuery query, CancellationToken ct)
    {
        Guid orderId = Guid.Parse(query.orderId);
        if (!await orderIService.IsFound(orderId, ct))
           return Result.Failure<GetOrderDetailsQueryResponse>(new Error("IOrderService.GetOrderDetails", $"Order With Id {query.orderId} not found", ErrorType.Conflict));
        var result = await orderIService.GetOrderDetails(orderId, ct);

        return result.IsFailure ? Result.Failure<GetOrderDetailsQueryResponse>(result.Errors)
            : Result.Success(GetOrderDetailsQueryResponse.map(result.Value));
    }
}
