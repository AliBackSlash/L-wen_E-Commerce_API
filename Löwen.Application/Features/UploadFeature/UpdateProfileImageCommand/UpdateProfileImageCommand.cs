namespace Löwen.Application.Features.UploadFeature.UpdateProfileImageCommand;

public record UpdateProfileImageCommand(string userId,string ProfileImagePath, string rootPath) : ICommand;
