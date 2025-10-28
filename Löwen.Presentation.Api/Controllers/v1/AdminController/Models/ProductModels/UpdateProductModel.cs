namespace Löwen.Presentation.Api.Controllers.v1.AdminController.Models.ProductModels;

public record UpdateProductModel(string Id,string? Name, string? Description, double? MainPrice, ProductStatus? Status, string? CategoryId);