namespace Löwen.Domain.Entities;

public class Size
{
    public Guid Id { get; set; }
    public string? SizeAsName { get; set; } 
    public byte? SizeAsNumber { get; set; }

    public ICollection<ProductVariant> ProductVariants { get; set; } = [];
}
