using Löwen.Domain.Entities.EntityForMapFunctionsResultOnly.Product;

namespace Löwen.Application.Features.ProductFeature.Queries;

public class GetProductByIdQueryResponse
{
    public Guid? ProductId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }
    public ProductStatus Status { get; set; }
    public double LoveCount { get; set; }
    public double? Discount { get; set; }
    public DiscountType? discountType { get; set; }

    public double? PriceAfterDiscount
    {
        get
        {
            return GetDiscount(this.Price, this.Discount, this.discountType);
        }
    }
    public required List<string>? ProductImage { get; set; }
    public double? Rating { get; set; }
    public bool IsFreeShipping => discountType == DiscountType.FreeShipping;

    private double? GetDiscount(double price, double? discount, DiscountType? discountType)
    {
        if (discount == null)
            return null;

        switch (discountType)
        {
            case DiscountType.Percentage:
                return price - ((price / 100) * discount);
            case DiscountType.FixedAmount:
                return price - discount;
            default:
                return 0;

        }
    }
}


