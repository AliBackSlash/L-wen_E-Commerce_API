namespace Löwen.Application.Features.AdminFeatures.Commands.ActivateMarkedAsDeleted;
public class ActivateMarkedUserAsDeletedCommandHandler(IAppUserService userService) : ICommandHandler<ActivateMarkedUserAsDeletedCommand>
{
    public async Task<Result> Handle(ActivateMarkedUserAsDeletedCommand command, CancellationToken ct)
    {
        return await userService.ActivateMarkedAsDeletedAsync(command.Id, UserRole.User);
    }
}
