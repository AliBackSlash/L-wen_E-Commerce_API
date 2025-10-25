using Löwen.Domain.Abstractions.IServices.IEmailServices;
using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
namespace Löwen.Application.Features.OrderFeature.Commands.UpdateOrderSataus.UpdateOrderStatus;

internal class UpdateOrderStatusCommandHandler(IOrderService orderService, IEmailService emailService, IAppUserService appUserService) : ICommandHandler<UpdateOrderStatusCommand>
{
    public async Task<Result> Handle(UpdateOrderStatusCommand command, CancellationToken ct)
    {
        var order = await orderService.GetByIdAsync(Guid.Parse(command.OrderId), ct);
        if (order is null)
            return Result.Failure(new Error("Order.UpdateOrderStatus", "Order not found", ErrorType.BadRequest));

        order.Status = command.Status;

        var updateResult = await orderService.UpdateAsync(order, ct);

        if (updateResult.IsFailure)
            return Result.Failure(updateResult.Errors);

        var sendEmail = emailService.SendOrderStatusAsync()
        return Result.Success();
    }
}
