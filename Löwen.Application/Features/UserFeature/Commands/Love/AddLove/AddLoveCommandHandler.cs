using Löwen.Domain.Abstractions.IServices.IEntitiesServices;

namespace Löwen.Application.Features.UserFeature.Commands.Love.AddLove;

internal class AddLoveCommandHandler(ILoveProductUserService loveProductUser,IProductService productService) : ICommandHandler<AddLoveCommand>
{
    public async Task<Result> Handle(AddLoveCommand command, CancellationToken ct)
    {
        Guid userId = Guid.Parse(command.userId);
        Guid productId = Guid.Parse(command.productId);

        if (await loveProductUser.IsFoundAsync(userId, productId, ct))
            return Result.Failure(new Error("LoveProductUser.Add", $"you already added love for this product", ErrorType.Conflict));


        if (!await productService.IsFound(productId, ct))
            return Result.Failure(new Error("LoveProductUser.Add", $"Product with Id {command.productId} not found", ErrorType.Conflict));

        var createResult = await loveProductUser.AddAsync(new LoveProductUser
        {
            UserId = userId,
            ProductId = productId
        },ct);

        if (createResult.IsFailure)
            return Result.Failure(createResult.Errors);

        return Result.Success();
        
    }
}
