namespace Löwen.Domain.Layer_Dtos.Delivery;

public class GetAssignedOrdersDto
{
    public required string ProductImageUrl { get; set; }
    public required string ProductName { get; set; }
    public required string AddressDetails { get; set; }
    public required string CustomerPhoneNum { get; set; }
    public decimal Price { get; set; }
    public short Quantity { get; set; }
}
