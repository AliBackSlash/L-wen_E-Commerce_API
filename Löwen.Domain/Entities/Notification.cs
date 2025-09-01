namespace Löwen.Domain.Entities;

public class Notification
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? Message { get; set; }
    public string? Type { get; set; }

}