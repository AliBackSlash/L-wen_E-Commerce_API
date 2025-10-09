using Löwen.Domain.Layer_Dtos.Product;

namespace Löwen.Application.Features.AdminFeature.Commands.Product.AddProduct;

public record AddProductCommand(string Name, string? Description,
    ProductStatus Status, string CategoryId,string CreatedBy, string Tags,IEnumerable<ProductVariantDto> VariantDtos) : ICommand<Guid>;
