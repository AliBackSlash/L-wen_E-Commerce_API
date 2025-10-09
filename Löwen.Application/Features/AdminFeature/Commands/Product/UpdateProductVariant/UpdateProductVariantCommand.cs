using Löwen.Domain.Layer_Dtos.Product;

namespace Löwen.Application.Features.AdminFeature.Commands.Product.UpdateProductVariant;

public record UpdateProductVariantCommand
    (string ProductId, string? ColorId, string? SizeId, decimal? Price, int? StockQuantity) : ICommand;
