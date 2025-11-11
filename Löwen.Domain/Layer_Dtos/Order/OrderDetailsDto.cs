namespace Löwen.Domain.Layer_Dtos.Order;

public class OrderDetailsDto
{
    public Guid Id { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }
    public IEnumerable<OrderDetailsItemsDto> items { get; set; } = [];
    
}

public class GetOrdersForUser
{

}