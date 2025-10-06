namespace Löwen.Domain.Entities;

// Images Table
public class Image
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string? Path { get; set; }
    public bool IsMain { get; set; } = false;

    // Navigation property
    public Product? Product { get; set; }
}
