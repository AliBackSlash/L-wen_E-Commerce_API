namespace Löwen.Domain.Entities;

public class Color
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string HexCode { get; set; } = "#FFFFFF";

    // Navigation
    public ICollection<ProductVariant> ProductVariants { get; set; } = [];
}
