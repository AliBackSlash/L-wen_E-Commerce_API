namespace Löwen.Application.Features.PaymentFeature.UpdatePaymentStatus;

public record UpdatePaymentStatusCommand(string orderId, PaymentStatus status) : ICommand;
