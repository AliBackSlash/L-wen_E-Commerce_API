namespace Löwen.Domain.Entities;

public class ProductDiscount
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public Guid DiscountId { get; set; }
    public Discount Discount { get; set; } = null!;
}