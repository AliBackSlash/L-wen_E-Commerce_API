using Löwen.Domain.Enums;

namespace Löwen.Domain.Entities;

public class Payment
{
    public Guid Id { get; set; }   
    public Guid OrderId { get; set; }  
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string? TransactionId { get; set; }  // Gateway transaction reference
    public PaymentStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }

    public Order Order { get; set; } = null!;
}
