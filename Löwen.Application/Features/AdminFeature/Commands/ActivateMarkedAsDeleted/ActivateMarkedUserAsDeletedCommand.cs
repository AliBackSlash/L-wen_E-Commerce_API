using Löwen.Application.Messaging.ICommand;

namespace Löwen.Application.Features.AdminFeatures.Commands.ActivateMarkedAsDeleted;
public record ActivateMarkedUserAsDeletedCommand(Guid Id) : ICommand;

