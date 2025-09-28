namespace Löwen.Application.Features.DiscountFeature.Commands.AddDiscount;

public record AddDiscountCommand(string Name, DiscountType DiscountType,
    decimal? DiscountValue, DateTime StartDate, DateTime EndDate, bool IsActive) : ICommand;
