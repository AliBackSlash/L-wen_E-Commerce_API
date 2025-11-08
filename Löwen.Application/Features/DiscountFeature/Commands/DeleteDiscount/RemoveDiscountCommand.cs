using Löwen.Application.Messaging.ICommand;

namespace Löwen.Application.Features.DiscountFeature.Commands.DeleteDiscount;

public record RemoveDiscountCommand(string Id) : ICommand;