using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
namespace Löwen.Application.Features.CartFeature.Commands.AddToCart;

internal class AddToCartCommandHandler(ICartService cartService) : ICommandHandler<AddToCartCommand>
{
    public async Task<Result> Handle(AddToCartCommand command, CancellationToken ct)
    {
        Guid userId = Guid.Parse(command.UserId);
        if (!await cartService.IsFound(userId, ct))
            await cartService.AddAsync(new Cart
            {
                UserId = userId,

            }, ct);

        var createResult = await cartService.AddAsync(new Cart
        {
            UserId = Guid.Parse(command.UserId),
            CartItems = command.items.Select(i => new CartItem
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
            }).ToList()
        }, ct);

        if (createResult.IsFailure)
            return Result.Failure(createResult.Errors);

        return Result.Success();
    }
}
