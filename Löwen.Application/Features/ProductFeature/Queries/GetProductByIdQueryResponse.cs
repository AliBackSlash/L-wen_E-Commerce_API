namespace Löwen.Application.Features.ProductFeature.Queries;

public class GetProductByIdQueryResponse
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }
    public ProductStatus Status { get; set; }
    public double LoveCount { get; set; }
    public double? PriceAfterDiscount { get; set; }
    public required List<string>? ProductImage { get; set; }
    public double? Rating {  get; set; }
    public IEnumerable<ProductReviewsRe> Reviews { get; set; } = [];
    
}
//make the get reviews alone 
public class ProductReviewsRe
{
    public string? UserImage { get; set; }
    public required string UserName { get; set; }
    public char Rating { get; set; }
    public string? Review { get; set; }
    public DateTime CreatedAt { get; set; }
}