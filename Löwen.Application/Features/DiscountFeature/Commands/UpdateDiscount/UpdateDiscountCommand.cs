namespace Löwen.Application.Features.DiscountFeature.Commands.UpdateDiscount;

public record UpdateDiscountCommand(string Id,string? Name, DiscountType? DiscountType,
    decimal? DiscountValue, DateTime? StartDate, DateTime? EndDate, bool? IsActive) : ICommand;