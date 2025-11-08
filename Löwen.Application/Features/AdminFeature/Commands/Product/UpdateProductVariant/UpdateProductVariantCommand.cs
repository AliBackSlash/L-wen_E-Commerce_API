using Löwen.Application.Messaging.ICommand;
using Löwen.Domain.Layer_Dtos.Product;

namespace Löwen.Application.Features.AdminFeature.Commands.Product.UpdateProductVariant;

public record UpdateProductVariantCommand
    (string PVId, string? ColorId, string? SizeId, decimal? Price, int? StockQuantity) : ICommand;
