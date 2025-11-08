using Löwen.Application.Messaging.ICommand;

namespace Löwen.Application.Features.DiscountFeature.Commands.AddDiscount;

public record AddDiscountCommand(string Name, DiscountType DiscountType,
    double? DiscountValue, DateTime StartDate, DateTime EndDate, bool IsActive) : ICommand;
