using Löwen.Domain.Abstractions.IServices.IEntitiesServices;

namespace Löwen.Application.Features.UserFeature.Commands.RemoveLove;

internal class RemoveLoveCommandHandler(IWishlistService wishlistService) : ICommandHandler<RemoveLoveCommand>
{
    public async Task<Result> Handle(RemoveLoveCommand command, CancellationToken ct)
    {
        var createResult = await wishlistService.AddAsync(new Wishlist
        {
            UserId = Guid.Parse(command.userId),
            ProductId = Guid.Parse(command.productId)
        },ct);

        if (createResult.IsFailure)
            return Result.Failure(createResult.Errors);

        return Result.Success();
        
    }
}
