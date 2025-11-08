using Löwen.Application.Messaging.ICommand;

namespace Löwen.Application.Features.RootAdminFeatures.Commands.AssignRole;
public record AssignRoleCommand(Guid Id, UserRole role) : ICommand;
