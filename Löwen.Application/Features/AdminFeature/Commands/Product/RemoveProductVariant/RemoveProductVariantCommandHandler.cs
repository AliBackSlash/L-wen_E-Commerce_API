using Löwen.Domain.Abstractions.IServices.IEntitiesServices;

namespace Löwen.Application.Features.AdminFeature.Commands.Product.RemoveProductVariant;

public class RemoveProductVariantCommandHandler(IProductService productService) : ICommandHandler<RemoveProductVariantCommand>
{
    public async Task<Result> Handle(RemoveProductVariantCommand command, CancellationToken ct)
    {
        return await productService.DeleteProductVariantAsync(Guid.Parse(command.Id), ct);
    }
}
