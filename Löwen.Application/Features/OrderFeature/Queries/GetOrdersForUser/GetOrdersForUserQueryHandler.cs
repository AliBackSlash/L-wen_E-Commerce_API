using Löwen.Application.Features.OrderFeature.Queries.OrderDetailsResponse;
using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.ConfigurationClasses.Pagination;
using Microsoft.Extensions.Options;

namespace Löwen.Application.Features.UserFeature.Queries.GetOrdersForUser;

internal class GetOrdersForUserQueryHandler(IOrderService orderIService,IOptions<PaginationSettings> options) : IQueryHandler<GetOrdersForUserQuery, PagedResult<GetOrderDetailsQueryResponse>>
{
    public async Task<Result<PagedResult<GetOrderDetailsQueryResponse>>> Handle(GetOrdersForUserQuery query, CancellationToken ct)
    {
        var result = await orderIService.GetOrdersForUser(Guid.Parse(query.userId), new PaginationParams
        {
            maxPageSize = options.Value.maxPageSize,
            PageNumber = query.PageNumber,
            Take = query.PageSize
        }, ct);

        return result.IsFailure ? Result.Failure<PagedResult<GetOrderDetailsQueryResponse>>(result.Errors)
            : Result.Success(PagedResult<GetOrderDetailsQueryResponse>
            .Create(GetOrderDetailsQueryResponse.map(result.Value.Items),result.Value.TotalCount ,result.Value.PageNumber,result.Value.PageSize));
    }
}
