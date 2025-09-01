namespace Löwen.Domain.Entities;

public class AdminLog
{
    public int Id { get; set; }
    public int AdminId { get; set; }
    public int ProductId { get; set; }
    public string? Action { get; set; }
    public DateTime CreatedAt { get; set; }

    public Product Product { get; set; } = null!;
}
