namespace Löwen.Domain.Entities;

public class ProductCategory
{
    public int Id { get; set; }
    public string? Category { get; set; }
    public string? ForGender { get; set; }
    public int? AgeFrom { get; set; }
    public int? AgeTo { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
