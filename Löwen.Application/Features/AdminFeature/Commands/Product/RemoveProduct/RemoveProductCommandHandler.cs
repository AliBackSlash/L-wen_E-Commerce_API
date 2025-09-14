using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.Entities;

namespace Löwen.Application.Features.AdminFeature.Commands.Product.RemoveProduct;

public class RemoveProductCommandHandler(IProductTagService tagService) : ICommandHandler<RemoveProductCommand>
{
    public async Task<Result> Handle(RemoveProductCommand command, CancellationToken ct)
    {
        
        var tag = await tagService.GetByIdAsync(command.Id, ct);
       
        if (tag == null)
            return Result.Failure(new Error("Tag.Delete", "Tag not found", ErrorType.Conflict));

        var updateResult = await tagService.DeleteAsync(tag, ct);

        if (updateResult.IsFailure)
            return Result.Failure(updateResult.Errors);

        return Result.Success();
    }
}
