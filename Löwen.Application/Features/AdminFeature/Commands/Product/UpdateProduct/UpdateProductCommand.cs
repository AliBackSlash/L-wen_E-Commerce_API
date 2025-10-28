namespace Löwen.Application.Features.AdminFeature.Commands.Product.UpdateProduct;

public record UpdateProductCommand(string Id, string? Name, string? Description, double? MainPrice, ProductStatus? Status, string? CategoryId) : ICommand;
