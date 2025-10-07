using Löwen.Domain.ErrorHandleClasses;
using Löwen.Presentation.API.UploadFilesServices;

namespace Löwen.Presentation.API.Services;

public interface IFileService
{
    Task<Result<UploadResponse>> UploadProfileImageAsync(IFormFile file);
    Task<Result<IEnumerable<(bool IsMain,string Path)>>> UploadProoductImagesAsync(IEnumerable<UploudPruductImages> files);
}
