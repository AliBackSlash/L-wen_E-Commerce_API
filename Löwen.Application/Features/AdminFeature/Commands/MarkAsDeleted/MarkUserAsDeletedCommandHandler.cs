using Löwen.Application.Messaging.ICommand;

namespace Löwen.Application.Features.AdminFeatures.Commands.MarkAsDeleted;
public class MarkUserAsDeletedCommandHandler(IAppUserService userService) : ICommandHandler<MarkUserAsDeletedCommand>
{
    public async Task<Result> Handle(MarkUserAsDeletedCommand command, CancellationToken ct)
    {
        return await userService.MarkAsDeletedAsync(command.Id, UserRole.User);
    }
}
