using Löwen.Domain.Abstractions.IServices.IAppUserServices;
using Löwen.Domain.Abstractions.IServices.IEmailServices;
using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.Entities;
namespace Löwen.Application.Features.OrderFeature.Commands.AssignedOrdersToDelivery;

internal class AssignedOrdersToDeliveryCommandHandler(IOrderService orderService, IAppUserService appUserService, IEmailService emailService) : ICommandHandler<AssignedOrdersToDeliveryCommand>
{
    public async Task<Result> Handle(AssignedOrdersToDeliveryCommand command, CancellationToken ct)
    {
        var createResult = await orderService.AssignedOrdersToDelivery(command.orders.Select(x => new DeliveryOrder
        {
            DeliveryId = x.DeliveryId,
            OrderId = x.OrderId
        }).ToList(), ct);

        if (createResult.IsFailure)
            return Result.Failure(createResult.Errors);
        // i will use mediator notification here 
        foreach (var order in command.orders)
        {

            var userInfo = await appUserService.GetNameAndEmailByUserIdFromOrderId(order.OrderId, ct);

            if (userInfo is not null)
                await emailService.SendOrderStatusAsync(userInfo.Value.Email, OrderStatus.Shipped, userInfo.Value.Name, ct);
        }

        return Result.Success();
    }
}
