using Löwen.Domain.Enums;

namespace Löwen.Domain.Entities;

// AdminLogs Table
public class AdminLog
{
    public Guid Id { get; set; }
    public Guid AdminId { get; set; }
    public Guid ProductId { get; set; }
    public ActionType ActionType { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public Product? Product { get; set; }
}

