namespace Löwen.Application.Features.UserFeature.Queries.GetOrderDetails;

public class OrderDetailsItems
{
    public Guid ProductId { get; set; }
    public byte Quantity { get; set; }
    public decimal PriceAtPurchase { get; set; }
}