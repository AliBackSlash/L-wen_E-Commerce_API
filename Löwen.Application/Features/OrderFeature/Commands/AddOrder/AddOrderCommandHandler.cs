using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
namespace Löwen.Application.Features.OrderFeature.Commands.AddOrder.AddOrder;

internal class AddOrderCommandHandler(IOrderService orderService) : ICommandHandler<AddOrderCommand>
{
    public async Task<Result> Handle(AddOrderCommand command, CancellationToken ct)
    {
        var createResult = await orderService.AddAsync(new Order
        {
            CustomerId = Guid.Parse(command.CustomerId),
            Status = OrderStatus.Pending,
            OrderItems = command.items.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                Price = i.PriceAtPurchase,
            }).ToList()
        }, ct);

        if (createResult.IsFailure)
            return Result.Failure(createResult.Errors);

        return Result.Success();
    }
}
