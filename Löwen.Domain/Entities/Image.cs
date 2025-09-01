namespace Löwen.Domain.Entities;

public class Image
{
    public int Id { get; set; }
    public string? Path { get; set; }

    public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
}
