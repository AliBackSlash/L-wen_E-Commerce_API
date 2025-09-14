using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.Entities;

namespace Löwen.Application.Features.AdminFeature.Commands.Tag.AddTag;

public class AddTagCommandHandler(IProductTagService TagService,IProductService productService) : ICommandHandler<AddTagCommand>
{
    public async Task<Result> Handle(AddTagCommand command, CancellationToken ct)
    {
        Guid ProductId = Guid.Parse(command.productId);

        if (await productService.IsFound(ProductId, ct))
            return Result.Failure(new Error("ProductTag.Add", $"Product with Id {command.productId} not found", ErrorType.Conflict));

        var addResult = await TagService.AddAsync(new ProductTag
        {
            Tag = command.Tag,
            ProductId = ProductId


        }, ct);

        if (addResult.IsFailure)
            return Result.Failure(addResult.Errors);

        return Result.Success();
    }
}
