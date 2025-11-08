using Löwen.Application.Messaging.ICommand;
using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
namespace Löwen.Application.Features.CartFeature.Commands.UpdateCartItemQuantity;

internal class UpdateCartItemQuantityCommandHandler(ICartService cartService) : ICommandHandler<UpdateCartItemQuantityCommand>
{
    public async Task<Result> Handle(UpdateCartItemQuantityCommand command, CancellationToken ct)
    {
        var updateResult = await cartService.UpdateCartItemQuantity(Guid.Parse(command.cartId), 
            Guid.Parse(command.productId),command.quantity, ct);

        if (updateResult.IsFailure)
            return Result.Failure(updateResult.Errors);

        return Result.Success();
    }
}
