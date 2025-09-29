using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
namespace Löwen.Application.Features.CartFeature.Commands.UpdateCartItemQuantity;

internal class RemoveFromCartItemCommandHandler(ICartService cartService) : ICommandHandler<RemoveFromCartItemCommand>
{
    public async Task<Result> Handle(RemoveFromCartItemCommand command, CancellationToken ct)
    {
        var updateResult = await cartService.UpdateCartItemQuantity(Guid.Parse(command.cartId), 
            Guid.Parse(command.productId),command.quantity, ct);

        if (updateResult.IsFailure)
            return Result.Failure(updateResult.Errors);

        return Result.Success();
    }
}
