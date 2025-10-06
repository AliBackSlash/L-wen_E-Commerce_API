using Löwen.Domain.Layer_Dtos.Product;

namespace Löwen.Application.Features.AdminFeature.Commands.Product.AddProductImages;

public record AddProductImagesCommand(IEnumerable<AddProductImagesDto> images) : ICommand;
