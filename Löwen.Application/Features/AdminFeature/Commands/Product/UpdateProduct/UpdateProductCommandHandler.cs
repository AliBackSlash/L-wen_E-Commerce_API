using Löwen.Domain.Abstractions.IServices.IEntitiesServices;

namespace Löwen.Application.Features.AdminFeature.Commands.Product.UpdateProduct;

public class UpdateProductCommandHandler(IProductTagService tagService) : ICommandHandler<UpdateProductCommand>
{
    public async Task<Result> Handle(UpdateProductCommand command, CancellationToken ct)
    {
        
        var tag = await tagService.GetByIdAsync(Guid.Parse(command.Id), ct);
       
        if (tag == null)
            return Result.Failure(new Error("ProductTag.Update", "Tag not found", ErrorType.Conflict));

        tag.Tag = command.Tag;
     
     
        var updateResult = await tagService.UpdateAsync(tag, ct);

        if (updateResult.IsFailure)
            return Result.Failure(updateResult.Errors);

        return Result.Success();
    }
}
