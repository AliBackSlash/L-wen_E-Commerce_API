using Löwen.Application.Features.CartFeature.Queries.GetCartByUser;
using Löwen.Application.Messaging.IQuery;
using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.ConfigurationClasses.Pagination;
using Löwen.Domain.Pagination;
using Microsoft.Extensions.Options;

namespace Löwen.Application.Features.DeliveryFeature.Queries.GetAssignedOrders;

internal class GetAssignedOrdersQueryHandler(IDeliveryService deliveryService, IOptions<PaginationSettings> options)
    : IQueryHandler<GetAssignedOrdersQuery, PagedResult<GetAssignedOrdersQueryresponse>>
{
    public async Task<Result<PagedResult<GetAssignedOrdersQueryresponse>>> Handle(GetAssignedOrdersQuery command, CancellationToken ct)
    {
        var items = await deliveryService.GetAssignedOrdersAsync(Guid.Parse(command.DeliveryId),
             new PaginationParams
             {
                 maxPageSize = options.Value.maxPageSize,
                 PageNumber = command.PageNumber,
                 Take = command.PageSize,
             }, ct);

        return Result.Success(PagedResult<GetAssignedOrdersQueryresponse>.Create(items.Items.Select(i => new GetAssignedOrdersQueryresponse
        {
            CustomerName = i.CustomerName,
            AddressDetails = i.AddressDetails,
            CustomerPhoneNum = i.CustomerPhoneNum,
            Total = i.Total,
            Status = i.Status
        })
         , items.TotalCount
         , items.PageNumber, items.PageSize));
    }
}
