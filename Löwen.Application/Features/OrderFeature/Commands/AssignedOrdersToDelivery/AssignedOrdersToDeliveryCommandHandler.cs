using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
namespace Löwen.Application.Features.OrderFeature.Commands.AssignedOrdersToDelivery;

internal class AssignedOrdersToDeliveryCommandHandler(IOrderService orderService) : ICommandHandler<AssignedOrdersToDeliveryCommand>
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

        return Result.Success();
    }
}
