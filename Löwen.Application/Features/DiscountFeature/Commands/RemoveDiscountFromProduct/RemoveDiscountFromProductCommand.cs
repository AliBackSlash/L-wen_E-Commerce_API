namespace Löwen.Application.Features.DiscountFeature.Commands.RemoveDiscountFromProduct;

public record RemoveDiscountFromProductCommand(string discountId,string ProductId) : ICommand;
