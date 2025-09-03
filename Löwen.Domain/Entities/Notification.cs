using Löwen.Domain.Enums;

namespace Löwen.Domain.Entities;

// Notifications Table
public class Notification
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Message { get; set; }
    public MessageType Type { get; set; }
}