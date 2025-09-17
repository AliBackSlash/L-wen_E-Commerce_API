
using Löwen.Application.Features.UserFeature.Commands.ChangePasswordCommand;
using Löwen.Domain.Abstractions.IServices.IAppUserServices;
using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.Entities;
using System.Xml.Linq;

namespace Löwen.Application.Features.UserFeature.Commands.AddToWishlist;

internal class AddToWishlistCommandHandler(IWishlistService wishlistService) : ICommandHandler<AddLoveCommand>
{
    public async Task<Result> Handle(AddLoveCommand command, CancellationToken ct)
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
