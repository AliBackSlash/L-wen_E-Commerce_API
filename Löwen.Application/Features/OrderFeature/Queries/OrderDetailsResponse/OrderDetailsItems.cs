namespace Löwen.Application.Features.OrderFeature.Queries.OrderDetailsResponse;

public class OrderDetailsItems
{
    public Guid ProductId { get; set; }
    public byte Quantity { get; set; }
    public decimal PriceAtPurchase { get; set; }
    public string Path { get; internal set; }
}