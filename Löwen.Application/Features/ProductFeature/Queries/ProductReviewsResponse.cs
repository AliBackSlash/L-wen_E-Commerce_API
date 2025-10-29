namespace Löwen.Application.Features.ProductFeature.Queries;

//make the get reviews alone 
public class ProductReviewsResponse
{
    public string? UserImage { get; set; }
    public required string UserName { get; set; }
    public char Rating { get; set; }
    public string? Review { get; set; }
    public DateTime CreatedAt { get; set; }
}