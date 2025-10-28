using Löwen.Domain.Layer_Dtos.Product;

namespace Löwen.Presentation.Api.Controllers.v1.AdminController.Models.ProductModels;

public record AddProductModel(string Name, string? Description,
    ProductStatus Status, string CategoryId,string Tags,double MainPrice, IEnumerable<ProductVariantDto> VariantDtos);
