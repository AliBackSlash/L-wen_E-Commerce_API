namespace Löwen.Domain.Entities;

public class CustomerAddress
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? Details { get; set; }
    public string? Status { get; set; }
    public bool IsActive { get; set; }

}
