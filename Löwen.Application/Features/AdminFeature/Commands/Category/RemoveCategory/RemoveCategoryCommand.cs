using Löwen.Application.Messaging.ICommand;

namespace Löwen.Application.Features.AdminFeature.Commands.Category.RemoveCategory;

public record RemoveCategoryCommand(Guid Id) : ICommand;
