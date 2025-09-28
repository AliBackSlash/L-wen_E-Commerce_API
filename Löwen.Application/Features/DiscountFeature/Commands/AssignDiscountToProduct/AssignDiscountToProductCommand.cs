namespace Löwen.Application.Features.DiscountFeature.Commands.AssignDiscountToProduct;

public record AssignDiscountToProductCommand(string discountId,string ProductId) : ICommand;
