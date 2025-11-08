using Löwen.Application.Messaging.ICommand;

namespace Löwen.Application.Features.RootAdminFeatures.Commands.MarkAsDeleted;
public class MarkAdminAsDeletedCommandHandler(IAppUserService userService) : ICommandHandler<MarkAdminAsDeletedCommand>
{
    public async Task<Result> Handle(MarkAdminAsDeletedCommand command, CancellationToken ct)
    {
        return await userService.MarkAsDeletedAsync(command.Id, UserRole.Admin);
    }
}
