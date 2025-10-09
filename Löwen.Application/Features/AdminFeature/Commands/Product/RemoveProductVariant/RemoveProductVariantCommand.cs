using Löwen.Domain.Layer_Dtos.Product;

namespace Löwen.Application.Features.AdminFeature.Commands.Product.RemoveProductVariant;

public record RemoveProductVariantCommand
    (string Id) : ICommand;
