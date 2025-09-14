namespace Löwen.Application.Features.AdminFeature.Commands.Product.AddProduct;

public record AddProductCommand(string Name, string? Description, decimal Price, short StockQuantity, ProductStatus Status, string CategoryId) : ICommand;
