using Löwen.Application.Messaging.IQuery;
using Löwen.Domain.Layer_Dtos.Cart;
using Löwen.Domain.Layer_Dtos.Order;

namespace Löwen.Application.Features.DeliveryFeature.Queries.GetAssignedOrders;

public record GetAssignedOrdersQuery(string DeliveryId, int PageNumber, byte PageSize) : IQuery<PagedResult<GetAssignedOrdersQueryresponse>>;
