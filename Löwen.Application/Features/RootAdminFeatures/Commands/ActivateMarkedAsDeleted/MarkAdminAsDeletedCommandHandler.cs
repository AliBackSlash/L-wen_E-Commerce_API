
namespace Löwen.Application.Features.RootAdminFeatures.Commands.ActivateMarkedAsDeleted;
public class ActivateMarkedAsDeletedCommandHandler(IAppUserService userService) : ICommandHandler<ActivateMarkedAsDeletedCommand>
{
    public async Task<Result> Handle(ActivateMarkedAsDeletedCommand command, CancellationToken cancellationToken)
    {
        return await userService.ActivateMarkedAsDeletedAsync(command.Id);
    }
}
