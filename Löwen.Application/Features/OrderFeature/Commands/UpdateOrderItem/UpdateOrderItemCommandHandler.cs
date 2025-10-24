using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
namespace Löwen.Application.Features.OrderFeature.Commands.UpdateOrderItem.UpdateOrderItem;

internal class UpdateOrderItemCommandHandler(IOrderService orderService,IOrderItemsService orderItemService) : ICommandHandler<UpdateOrderItemCommand>
{
    public async Task<Result> Handle(UpdateOrderItemCommand command, CancellationToken ct)
    {
        Guid orderId = Guid.Parse(command.orderId);
        var orderitem = await orderItemService.GetOrderItem(orderId, Guid.Parse(command.productId), ct);

        if (orderitem is null)
            return Result.Failure(new Error("Order.UpdateOrderItem", "Order Item not found", ErrorType.BadRequest));

        if (command.deliveryId is not null)
        {
            var order = await orderService.GetByIdAsync(orderId,ct);

            if(order is null)
                return Result.Failure(new Error("Order.UpdateOrderItem", "Order not found", ErrorType.BadRequest));

            order.CustomerId = Guid.Parse(command.deliveryId);

           var re = await orderService.UpdateAsync(order, ct);
            if (re.IsFailure)
                return re;
        }

        orderitem.Quantity = command.Quantity ?? orderitem.Quantity;
        orderitem.Price = command.PriceAtPurchase ?? orderitem.Price;

        var updateResult = await orderItemService.UpdateAsync(orderitem, ct);

        if (updateResult.IsFailure)
            return Result.Failure(updateResult.Errors);

        return Result.Success();
    }
}
