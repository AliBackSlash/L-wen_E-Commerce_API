using Löwen.Application.Messaging.ICommand;

namespace Löwen.Application.Features.RootAdminFeatures.Commands.MarkAsDeleted;
public record MarkAdminAsDeletedCommand(Guid Id) : ICommand;

