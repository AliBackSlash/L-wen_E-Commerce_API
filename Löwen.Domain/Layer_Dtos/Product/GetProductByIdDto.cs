namespace Löwen.Domain.Layer_Dtos.Product;

public class GetProductByIdDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }
    public ProductStatus Status { get; set; }
    public double LoveCount { get; set; }
    public double? PriceAfterDiscount { get; set; }
    public List<string>? ProductImage { get; set; }
    public double? Rating { get; set; }

    public IEnumerable<ProductReviewsDto> Reviews { get; set; } = [];
}
public class ProductReviewsDto
{
    public string? UserImage { get; set; }
    public required string UserName { get; set; }
    public char Rating { get; set; }
    public string? Review { get; set; }
    public DateTime CreatedAt { get; set; }
}