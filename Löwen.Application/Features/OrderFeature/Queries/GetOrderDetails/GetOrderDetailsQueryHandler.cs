using Löwen.Application.Features.OrderFeature.Queries.OrderDetailsResponse;
using Löwen.Domain.Abstractions.IServices.IEntitiesServices;

namespace Löwen.Application.Features.UserFeature.Queries.GetOrderDetails;

internal class GetOrderDetailsQueryHandler(IOrderService orderIService) : IQueryHandler<GetOrderDetailsQuery, GetOrderDetailsQueryResponse>
{
    public async Task<Result<GetOrderDetailsQueryResponse>> Handle(GetOrderDetailsQuery query, CancellationToken ct)
    {
        var result = await orderIService.GetOrderDetails(Guid.Parse(query.orderId), ct);

        return result.IsFailure ? Result.Failure<GetOrderDetailsQueryResponse>(result.Errors)
            : Result.Success(GetOrderDetailsQueryResponse.map(result.Value));
    }
}
