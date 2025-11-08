
using Löwen.Application.Messaging.ICommand;
using Löwen.Domain.Abstractions.IServices.IAppUserServices;

namespace Löwen.Application.Features.RootAdminFeatures.Commands.RemoveAdminCommand;
public class RemoveAdminCommandHandler(IAppUserService userService) : ICommandHandler<RemoveAdminCommand>
{
    public async Task<Result> Handle(RemoveAdminCommand command, CancellationToken ct)
    {
        return await userService.RemoveUserAsync(command.Id.ToString());
    }
}
