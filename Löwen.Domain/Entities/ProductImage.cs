namespace Löwen.Domain.Entities;

public class ProductImage
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int ImageId { get; set; }

    public Product Product { get; set; } = null!;
    public Image Image { get; set; } = null!;
}
