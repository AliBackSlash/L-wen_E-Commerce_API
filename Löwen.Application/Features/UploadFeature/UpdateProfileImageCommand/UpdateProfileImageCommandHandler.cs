

using Löwen.Application.Messaging.ICommand;
using Löwen.Domain.Abstractions.IServices.IAppUserServices;

namespace Löwen.Application.Features.UploadFeature.UpdateProfileImageCommand;

internal class UpdateProfileImageCommandHandler(IAppUserService userService) : ICommandHandler<UpdateProfileImageCommand>
{

    public async Task<Result> Handle(UpdateProfileImageCommand command, CancellationToken ct)
    {
        return await userService.UpdateProfileImageAsync(command.userId, command.ProfileImagePath,command.rootPath);
    }
}
