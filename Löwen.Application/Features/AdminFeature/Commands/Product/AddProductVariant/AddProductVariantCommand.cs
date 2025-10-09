using Löwen.Domain.Layer_Dtos.Product;

namespace Löwen.Application.Features.AdminFeature.Commands.Product.AddProductVariant;

public record AddProductVariantCommand(string ProductId, string ColorId, string SizeId, decimal Price, int StockQuantity) : ICommand;
