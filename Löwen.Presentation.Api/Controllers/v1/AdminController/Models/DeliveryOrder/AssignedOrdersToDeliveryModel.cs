namespace Löwen.Presentation.Api.Controllers.v1.AdminController.Models.DeliveryOrder;

public class AssignedOrdersToDeliveryModel
{
  public IEnumerable<DeliveryOrderDto> deliveryOrders { get; set; } = [];
}
