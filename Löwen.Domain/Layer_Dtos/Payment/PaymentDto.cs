namespace Löwen.Domain.Layer_Dtos.Payment;

public class PaymentDto
{
    public Guid OrderId { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string? TransactionId { get; set; }  // Gateway transaction reference
    public PaymentStatus Status { get; set; }
}
