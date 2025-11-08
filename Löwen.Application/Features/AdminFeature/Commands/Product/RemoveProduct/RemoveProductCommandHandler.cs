using Löwen.Application.Messaging.ICommand;
using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.Entities;

namespace Löwen.Application.Features.AdminFeature.Commands.Product.RemoveProduct;

public class RemoveProductCommandHandler(IProductService productService) : ICommandHandler<RemoveProductCommand>
{
    public async Task<Result> Handle(RemoveProductCommand command, CancellationToken ct)
    {
       /* if(Guid.TryParse(command.Id,out Guid Id))
            return Result.Failure(new Error("Tag.Delete", "Invalid guid Id", ErrorType.Conflict));
*/
        var tag = await productService.GetByIdAsync(Guid.Parse(command.Id), ct);
       
        if (tag == null)
            return Result.Failure(new Error("product.Delete", "product not found", ErrorType.Conflict));

        var updateResult = await productService.DeleteAsync(tag, ct);

        if (updateResult.IsFailure)
            return Result.Failure(updateResult.Errors);

        return Result.Success();
    }
}
