using Löwen.Domain.Layer_Dtos.Order;

namespace Löwen.Application.Features.OrderFeature.Queries.OrderDetailsResponse;

public class GetOrderDetailsQueryResponse
{
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }
    public IEnumerable<OrderDetailsItems> items { get; set; } = [];
    public decimal Total => items.Sum(x => x.Quantity * x.Price);
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
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                Price = item.Price,
                Path = item.Path,
            }).AsEnumerable()
        };
    }
    public static IEnumerable<GetOrderDetailsQueryResponse> map(IEnumerable<OrderDetailsDto> dto)
    {
       return dto.Select(i => map(i));
    }
}
