using Löwen.Domain.ConfigurationClasses.ApiSettings;
using Löwen.Domain.ConfigurationClasses.StaticFilesHelpersClasses;
using Löwen.Domain.Layer_Dtos.Cart;
using Microsoft.Extensions.Options;

namespace Löwen.Application.Features.DeliveryFeature.Queries.GetAssignedOrders;

public class GetAssignedOrdersQueryresponse
{
    public required string ProductImageUrl { get; set; }
    public required string ProductName { get; set; }
    public required string AddressDetails { get; set; }
    public required string CustomerPhoneNum { get; set; }
    public decimal Price { get; set; }
    public short Quantity {  get; set; }
    public decimal PriceTotal { get { return Price * Quantity; } }

}
