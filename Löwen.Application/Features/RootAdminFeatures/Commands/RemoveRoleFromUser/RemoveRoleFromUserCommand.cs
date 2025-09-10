namespace Löwen.Application.Features.RootAdminFeatures.Commands.RemoveRoleFromUser;
public record RemoveRoleFromUserCommand(Guid Id, UserRole role) : ICommand;
