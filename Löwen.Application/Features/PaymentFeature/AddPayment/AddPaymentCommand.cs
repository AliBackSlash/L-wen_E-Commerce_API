using Löwen.Application.Messaging.ICommand;
using Löwen.Domain.Layer_Dtos.Order;

namespace Löwen.Application.Features.PaymentFeature.AddPayment;

public record AddPaymentCommand(string OrderId, decimal Amount, PaymentMethod PaymentMethod, string? TransactionId, PaymentStatus Status) : ICommand;
