using Löwen.Domain.Abstractions.IServices.IEntitiesServices;

namespace Löwen.Application.Features.AdminFeature.Commands.Product.UpdateProductVariant;

public class UpdateProductVariantCommandHandler(IProductService productService) : ICommandHandler<UpdateProductVariantCommand>
{
    public async Task<Result> Handle(UpdateProductVariantCommand command, CancellationToken ct)
    {

        return await productService.UpdateProductVariantAsync(Guid.Parse(command.ProductId),
            new Domain.Layer_Dtos.Product.UpdateProductVariantDto
            {
                ColorId = command.ColorId is null ? null : Guid.Parse(command.ColorId),
                SizeId = command.SizeId is null ? null : Guid.Parse(command.SizeId),
                StockQuantity = command.StockQuantity,
                Price = command.Price,
            }, ct);
    }
}
