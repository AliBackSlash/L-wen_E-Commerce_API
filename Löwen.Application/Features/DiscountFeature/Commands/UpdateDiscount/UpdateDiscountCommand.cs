using Löwen.Application.Messaging.ICommand;

namespace Löwen.Application.Features.DiscountFeature.Commands.UpdateDiscount;

public record UpdateDiscountCommand(string Id,string? Name, DiscountType? DiscountType,
    double? DiscountValue, DateTime? StartDate, DateTime? EndDate, bool? IsActive) : ICommand;