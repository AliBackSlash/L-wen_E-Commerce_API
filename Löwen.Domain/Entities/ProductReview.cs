namespace Löwen.Domain.Entities;

// ProductReviews Table
public class ProductReview
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public char Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public Product? Product { get; set; }
}
