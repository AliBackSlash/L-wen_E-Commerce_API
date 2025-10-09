using Löwen.Domain.Abstractions.IServices.IEntitiesServices;

namespace Löwen.Application.Features.AdminFeature.Commands.Tag.UpdateTag;

public class UpdateTagCommandHandler(IProductTagService tagService) : ICommandHandler<UpdateTagCommand>
{
    public async Task<Result> Handle(UpdateTagCommand command, CancellationToken ct)
    {
        
        var tag = await tagService.GetByProductIdAsync(Guid.Parse(command.Id), ct);
       
        if (tag == null)
            return Result.Failure(new Error("ProductTag.Update", "Tag not found", ErrorType.Conflict));

        tag.Tag = command.Tag;
     
     
        var updateResult = await tagService.UpdateAsync(tag, ct);

        if (updateResult.IsFailure)
            return Result.Failure(updateResult.Errors);

        return Result.Success();
    }
}
