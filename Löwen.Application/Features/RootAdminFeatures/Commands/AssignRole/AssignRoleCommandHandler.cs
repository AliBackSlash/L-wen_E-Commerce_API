using Löwen.Application.Abstractions.IServices.IdentityServices;

namespace Löwen.Application.Features.RootAdminFeatures.Commands.AssignRole;

public class AssignRoleCommandHandler(IAppUserService userService) : ICommandHandler<AssignRoleCommand>
{
    public async Task<Result> Handle(AssignRoleCommand command, CancellationToken cancellationToken)
    {
        return await userService.AssignUserToRoleAsync(command.Id, command.role);
    }
}
