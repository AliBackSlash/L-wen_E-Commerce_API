
namespace Löwen.Domain.Layer_Dtos.Delivery;

public class GetAssignedOrdersDto
{
    public required string CustomerName { get; set; }
    public required string AddressDetails { get; set; }
    public required string CustomerPhoneNum { get; set; }
    public decimal Total { get; set; }
    public OrderStatus Status { get; set; }
}
