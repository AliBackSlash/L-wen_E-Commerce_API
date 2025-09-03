using Löwen.Application.Abstractions.IServices.IdentityServices;

namespace Löwen.Application.Features.UploadFeature.UpdateProfileImageCommand;

internal class UpdateProfileImageCommandHandler(IAppUserService userService) : ICommandHandler<UpdateProfileImageCommand>
{

    public async Task<Result> Handle(UpdateProfileImageCommand command, CancellationToken cancellationToken)
    {
        return await userService.UpdateProfileImageAsync(command.userId, command.ProfileImagePath,command.rootPath);
    }
}
