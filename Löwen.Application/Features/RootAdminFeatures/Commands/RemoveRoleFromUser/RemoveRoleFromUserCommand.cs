using Löwen.Application.Messaging.ICommand;

namespace Löwen.Application.Features.RootAdminFeatures.Commands.RemoveRoleFromUser;
public record RemoveRoleFromUserCommand(Guid Id, UserRole role) : ICommand;
