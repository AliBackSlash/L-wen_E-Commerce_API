using Löwen.Domain.Layer_Dtos.Cart;
using Löwen.Domain.Layer_Dtos.Order;

namespace Löwen.Application.Features.DeliveryFeature.Queries.GetAssignedOrders;

public record GetAssignedOrdersQuery(string DeliveryId) : IQuery<PagedResult<GetAssignedOrdersQueryresponse>>;
