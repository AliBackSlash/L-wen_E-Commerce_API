using Löwen.Application.Messaging.ICommand;
using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
namespace Löwen.Application.Features.CartFeature.Commands.RemoveFromCartItem;

internal class RemoveFromCartItemCommandHandler(ICartService cartService) : ICommandHandler<RemoveFromCartItemCommand>
{
    public async Task<Result> Handle(RemoveFromCartItemCommand command, CancellationToken ct)
    {
        var removeResult = await cartService.RemoveCartItem(Guid.Parse(command.cartId), Guid.Parse(command.productId), ct);

        if (removeResult.IsFailure)
            return Result.Failure(removeResult.Errors);

        return Result.Success();
    }
}
