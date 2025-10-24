using Löwen.Domain.ConfigurationClasses.ApiSettings;
using Löwen.Domain.ConfigurationClasses.StaticFilesHelpersClasses;
using Löwen.Domain.Layer_Dtos.Cart;
using Microsoft.Extensions.Options;

namespace Löwen.Application.Features.DeliveryFeature.Queries.GetAssignedOrders;

public class GetAssignedOrdersQueryresponse
{
    public required string CustomerName { get; set; }
    public required string AddressDetails { get; set; }
    public required string CustomerPhoneNum { get; set; }
    public decimal Total { get; set; }
    public OrderStatus Status { get; set; }

}
