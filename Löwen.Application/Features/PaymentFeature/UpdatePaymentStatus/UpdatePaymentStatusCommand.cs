using Löwen.Application.Messaging.ICommand;

namespace Löwen.Application.Features.PaymentFeature.UpdatePaymentStatus;

public record UpdatePaymentStatusCommand(string orderId, PaymentStatus status) : ICommand;
