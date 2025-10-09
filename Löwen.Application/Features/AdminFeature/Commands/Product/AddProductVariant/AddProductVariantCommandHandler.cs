using Löwen.Domain.Abstractions.IServices.IEntitiesServices;

namespace Löwen.Application.Features.AdminFeature.Commands.Product.AddProductVariant;

public class AddProductVariantCommandHandler(IProductService productService) : ICommandHandler<AddProductVariantCommand>
{
    public async Task<Result> Handle(AddProductVariantCommand command, CancellationToken ct)
    {
        return await productService.AddProductVariantAsync(Guid.Parse(command.ProductId),
            new Domain.Layer_Dtos.Product.ProductVariantDto
            {
                ColorId = Guid.Parse(command.ColorId),
                SizeId = Guid.Parse(command.SizeId),
                StockQuantity = command.StockQuantity,
                Price = command.Price,
            }, ct);

    }
}
