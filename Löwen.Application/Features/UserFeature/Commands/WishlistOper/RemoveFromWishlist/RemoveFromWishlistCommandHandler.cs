using Löwen.Domain.Abstractions.IServices.IEntitiesServices;

namespace Löwen.Application.Features.UserFeature.Commands.WishlistOper.RemoveFromWishlist;

internal class RemoveFromWishlistCommandHandler(IWishlistService wishlistService) : ICommandHandler<RemoveFromWishlistCommand>
{
    public async Task<Result> Handle(RemoveFromWishlistCommand command, CancellationToken ct)
    {
        
        var RemoveResult = await wishlistService.DeleteAsync(
            Guid.Parse(command.userId),
            Guid.Parse(command.productId)
        , ct);

        if (RemoveResult.IsFailure)
            return Result.Failure(RemoveResult.Errors);

        return Result.Success();
        
    }
}
