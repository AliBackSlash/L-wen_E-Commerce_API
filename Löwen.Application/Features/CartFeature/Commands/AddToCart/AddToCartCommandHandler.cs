using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
namespace Löwen.Application.Features.CartFeature.Commands.AddToCart;

internal class AddToCartCommandHandler(ICartService cartService) : ICommandHandler<AddToCartCommand>
{
    public async Task<Result> Handle(AddToCartCommand command, CancellationToken ct)
    {
        
        Guid userId = Guid.Parse(command.UserId);
        if (!await cartService.IsUserHasCart(userId, ct))
            await cartService.AddAsync(new Cart
            {
                UserId = userId,

            }, ct);

        var cartId = await cartService.GetCartIdByUserId(userId, ct);
        if(cartId is null)
            return Result.Failure(Error.NotFound("ICartService.AddToCart",$"Cart for user id {userId} not found"));


        return await cartService.AddToCartAsync(new CartItem {
            CartId = cartId.Value,
            ProductId = Guid.Parse(command.ProductId),
            Quantity = command.Quantity
        }, ct);

    }
}
