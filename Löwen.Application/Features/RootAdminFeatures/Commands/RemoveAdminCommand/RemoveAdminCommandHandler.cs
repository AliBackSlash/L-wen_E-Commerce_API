
namespace Löwen.Application.Features.RootAdminFeatures.Commands.RemoveAdminCommand;
public class RemoveAdminCommandHandler(IAppUserService userService) : ICommandHandler<RemoveAdminCommand>
{
    public async Task<Result> Handle(RemoveAdminCommand command, CancellationToken cancellationToken)
    {
        return await userService.RemoveUserAsync(command.Id);
    }
}
