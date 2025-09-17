namespace Löwen.Domain.Entities;

public class LoveProductUser
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }

    // Navigation properties
    public Product? Product { get; set; }
}
