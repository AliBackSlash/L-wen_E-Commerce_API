using Löwen.Application.Messaging.ICommand;
using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
namespace Löwen.Application.Features.PaymentFeature.AddPayment;

internal class AddPaymentCommandHandler(IPaymentService paymentService) : ICommandHandler<AddPaymentCommand>
{
    public async Task<Result> Handle(AddPaymentCommand command, CancellationToken ct)
    {
        var createResult = await paymentService.AddAsync(new Payment
        {
            OrderId = Guid.Parse(command.OrderId),
            PaymentMethod = command.PaymentMethod,
            TransactionId = command.TransactionId,
            Amount = command.Amount,
            Status = command.Status,
        } , ct);

        if (createResult.IsFailure)
            return Result.Failure(createResult.Errors);

        return Result.Success();
    }
}
