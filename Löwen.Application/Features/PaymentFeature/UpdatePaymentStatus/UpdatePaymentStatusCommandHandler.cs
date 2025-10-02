using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
namespace Löwen.Application.Features.PaymentFeature.UpdatePaymentStatus;

internal class UpdatePaymentStatusCommandHandler(IPaymentService paymentService) : ICommandHandler<UpdatePaymentStatusCommand>
{
    public async Task<Result> Handle(UpdatePaymentStatusCommand command, CancellationToken ct)
    {
        var updateResult = await paymentService.UpdateStatus(Guid.Parse(command.orderId), command.status, ct);

        if (updateResult.IsFailure)
            return Result.Failure(updateResult.Errors);

        return Result.Success();
    }
}
