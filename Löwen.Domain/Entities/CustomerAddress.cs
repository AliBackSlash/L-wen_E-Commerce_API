namespace Löwen.Domain.Entities;

// CustomerAddresses Table
public class CustomerAddress
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Details { get; set; }
    public bool IsActive { get; set; }

    // Navigation property
}
