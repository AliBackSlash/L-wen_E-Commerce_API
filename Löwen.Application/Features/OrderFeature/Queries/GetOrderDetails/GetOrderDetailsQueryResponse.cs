using Löwen.Domain.Layer_Dtos.Order;

namespace Löwen.Application.Features.UserFeature.Queries.GetOrderDetails;

public class GetOrderDetailsQueryResponse
{
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }
    public IEnumerable<OrderDetailsItems> items { get; set; } = [];

    private GetOrderDetailsQueryResponse() { }

    public static GetOrderDetailsQueryResponse map(OrderDetailsDto dto)
    {
        return new GetOrderDetailsQueryResponse
        {
            OrderDate = dto.OrderDate,
            Status = dto.Status,
            items = dto.items.Select(item => new OrderDetailsItems
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                PriceAtPurchase = item.PriceAtPurchase
            }).AsEnumerable()
        };
    }
}
