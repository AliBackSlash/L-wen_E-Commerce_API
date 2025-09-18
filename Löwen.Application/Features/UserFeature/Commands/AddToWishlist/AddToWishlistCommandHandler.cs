
using Löwen.Application.Features.UserFeature.Commands.ChangePasswordCommand;
using Löwen.Domain.Abstractions.IServices.IAppUserServices;
using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.Entities;
using System.Xml.Linq;
using static Löwen.Domain.ErrorHandleClasses.ErrorCodes;

namespace Löwen.Application.Features.UserFeature.Commands.AddToWishlist;

internal class AddToWishlistCommandHandler(IWishlistService wishlistService,IProductService productService) : ICommandHandler<AddToWishlistCommand>
{
    public async Task<Result> Handle(AddToWishlistCommand command, CancellationToken ct)
    {

        Guid userId = Guid.Parse(command.userId);
        Guid productId = Guid.Parse(command.productId);

        if (await wishlistService.IsFoundAsync(userId, productId, ct))
            return Result.Failure(new Error("Wishlist.Add", $"you already added this product", ErrorType.Conflict));


        if (!await productService.IsFound(productId, ct))
            return Result.Failure(new Error("Wishlist.Add", $"Product with Id {command.productId} not found", ErrorType.Conflict));

        var createResult = await wishlistService.AddAsync(new Wishlist
        {
            UserId = userId,
            ProductId = productId
        },ct);

        if (createResult.IsFailure)
            return Result.Failure(createResult.Errors);

        return Result.Success();
        
    }
}
