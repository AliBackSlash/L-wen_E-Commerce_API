using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
namespace Löwen.Application.Features.UserFeature.Commands.UpdateOrderItem.UpdateOrderItem;

internal class UpdateOrderItemCommandHandler(IOrderItemsService orderItemService) : ICommandHandler<UpdateOrderItemCommand>
{
    public async Task<Result> Handle(UpdateOrderItemCommand command, CancellationToken ct)
    {
        var orderitem = await orderItemService.GetOrderItem(Guid.Parse(command.orderId), Guid.Parse(command.productId), ct);
        if (orderitem is null)
            return Result.Failure(new Error("Order.UpdateOrderItem", "Order Item not found", ErrorType.BadRequest));

        orderitem.Quantity = command.Quantity ?? orderitem.Quantity;
        orderitem.PriceAtPurchase = command.PriceAtPurchase ?? orderitem.PriceAtPurchase;

        var updateResult = await orderItemService.UpdateAsync(orderitem, ct);

        if (updateResult.IsFailure)
            return Result.Failure(updateResult.Errors);

        return Result.Success();
    }
}
