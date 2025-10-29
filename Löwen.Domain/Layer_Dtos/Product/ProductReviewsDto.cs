namespace Löwen.Domain.Layer_Dtos.Product;

public class ProductReviewsDto
{
    public string? UserImage { get; set; }
    public required string UserName { get; set; }
    public char Rating { get; set; }
    public string? Review { get; set; }
    public DateTime CreatedAt { get; set; }
}