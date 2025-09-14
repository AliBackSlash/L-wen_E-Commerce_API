namespace Löwen.Application.Features.AdminFeature.Commands.Product.UpdateProduct;

public record UpdateProductCommand(string Id, string Tag) : ICommand;
