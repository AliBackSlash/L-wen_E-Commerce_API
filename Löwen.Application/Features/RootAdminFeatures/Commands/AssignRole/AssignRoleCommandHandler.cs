

using Löwen.Domain.Abstractions.IServices.IAppUserServices;

namespace Löwen.Application.Features.RootAdminFeatures.Commands.AssignRole;

public class AssignRoleCommandHandler(IAppUserService userService) : ICommandHandler<AssignRoleCommand>
{
    public async Task<Result> Handle(AssignRoleCommand command, CancellationToken ct)
    {
        return await userService.AssignUserToRoleAsync(command.Id, command.role);
    }
}
