namespace Löwen.Domain.Entities;

public class ProductTag
{
    public int Id { get; set; }
    public string? Tag { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
