namespace Löwen.Presentation.Api.Controllers.v1.AdminController.Models;

public record AddProductModel(string Name, string? Description, decimal Price, short StockQuantity, ProductStatus Status, string CategoryId);
