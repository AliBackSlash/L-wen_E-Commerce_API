namespace Löwen.Presentation.Api.Controllers.v1.PaymentController.Models;

public record AddPaymentModel(string OrderId, decimal Amount, 
    PaymentMethod PaymentMethod, string? TransactionId, PaymentStatus Status);
