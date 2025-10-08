using Löwen.Domain.Layer_Dtos.Product;

namespace Löwen.Application.Features.AdminFeature.Commands.Product.DeleteProductImages;

public record DeleteProductImagesCommand(string imageName) : ICommand;
