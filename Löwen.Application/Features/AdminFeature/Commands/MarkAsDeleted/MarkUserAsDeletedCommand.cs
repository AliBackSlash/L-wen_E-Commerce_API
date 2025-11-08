using Löwen.Application.Messaging.ICommand;

namespace Löwen.Application.Features.AdminFeatures.Commands.MarkAsDeleted;
public record MarkUserAsDeletedCommand(Guid Id) : ICommand;

