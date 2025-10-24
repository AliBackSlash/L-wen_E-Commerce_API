using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.ConfigurationClasses.Pagination;
using Löwen.Domain.Pagination;
using Microsoft.Extensions.Options;

namespace Löwen.Application.Features.DeliveryFeature.Queries.GetAssignedOrders;

internal class GetAssignedOrdersQueryHandler(ICartService cartService, IOptions<PaginationSettings> options)
    : IQueryHandler<GetAssignedOrdersQuery, PagedResult<GetAssignedOrdersQueryresponse>>
{
    public async Task<Result<PagedResult<GetAssignedOrdersQueryresponse>>> Handle(GetAssignedOrdersQuery command, CancellationToken ct)
    {
      throw new NotImplementedException(); ;
    }
}
