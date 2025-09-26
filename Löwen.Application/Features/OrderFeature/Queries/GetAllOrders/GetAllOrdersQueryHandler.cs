using Löwen.Application.Features.OrderFeature.Queries.OrderDetailsResponse;
using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.ConfigurationClasses.Pagination;
using Microsoft.Extensions.Options;

namespace Löwen.Application.Features.UserFeature.Queries.GetAllOrders;

internal class GetAllOrdersQueryHandler(IOrderService orderIService,IOptions<PaginationSettings> options) : IQueryHandler<GetAllOrdersQuery, PagedResult<GetOrderDetailsQueryResponse>>
{
    public async Task<Result<PagedResult<GetOrderDetailsQueryResponse>>> Handle(GetAllOrdersQuery query, CancellationToken ct)
    {
        var result = await orderIService.GetAllOrders(new PaginationParams
        {
            maxPageSize = options.Value.maxPageSize,
            PageNumber = query.PageNumber,
            PageSize = query.PageSize
        }, ct);

        return result.IsFailure ? Result.Failure<PagedResult<GetOrderDetailsQueryResponse>>(result.Errors)
            : Result.Success(PagedResult<GetOrderDetailsQueryResponse>
            .Create(GetOrderDetailsQueryResponse.map(result.Value.Items),result.Value.TotalCount ,result.Value.PageNumber,result.Value.PageSize));
    }
}
