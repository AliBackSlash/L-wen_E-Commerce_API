namespace Löwen.Application.Features.AdminFeature.Commands.Product.UpdateProduct;

public record UpdateProductCommand(string Id, string? Name, string? Description, decimal? Price, short? StockQuantity, ProductStatus? Status, string? CategoryId) : ICommand;
