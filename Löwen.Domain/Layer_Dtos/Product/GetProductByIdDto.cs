namespace Löwen.Domain.Layer_Dtos.Product;

public class GetProductByIdDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }
    public ProductStatus Status { get; set; }
    public double LoveCount { get; set; }
    public double? Discount { get; set; }
    public DiscountType? DiscountType { get; set; }
    public List<string>? ProductImage { get; set; }
    public double? Rating { get; set; }
}
