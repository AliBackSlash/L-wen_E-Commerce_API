using Löwen.Domain.Abstractions.IServices.IEntitiesServices;

namespace Löwen.Application.Features.UserFeature.Commands.RemoveLove;

internal class RemoveLoveCommandHandler(ILoveProductUserService loveProductUser) : ICommandHandler<RemoveLoveCommand>
{
    public async Task<Result> Handle(RemoveLoveCommand command, CancellationToken ct)
    {
        var removeResult = await loveProductUser.DeleteAsync(
            Guid.Parse(command.userId),
            Guid.Parse(command.productId)
        ,ct);

        if (removeResult.IsFailure)
            return Result.Failure(removeResult.Errors);

        return Result.Success();
        
    }
}
