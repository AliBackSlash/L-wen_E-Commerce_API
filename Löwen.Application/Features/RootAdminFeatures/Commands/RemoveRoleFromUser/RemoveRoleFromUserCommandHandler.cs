
using Löwen.Application.Messaging.ICommand;
using Löwen.Domain.Abstractions.IServices.IAppUserServices;

namespace Löwen.Application.Features.RootAdminFeatures.Commands.RemoveRoleFromUser;

public class RemoveRoleFromUserCommandHandler(IAppUserService userService) : ICommandHandler<RemoveRoleFromUserCommand>
{
    public async Task<Result> Handle(RemoveRoleFromUserCommand command, CancellationToken ct)
    {
        return await userService.RemoveRoleFromUserAsync(command.Id, command.role);
    }
}
