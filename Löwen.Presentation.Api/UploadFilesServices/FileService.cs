using Löwen.Domain.ErrorHandleClasses;
using Löwen.Domain.StaticFilesHelpersClasses;
using Löwen.Presentation.API.UploadFilesServices;
using Microsoft.Extensions.Options;
using System.Runtime;

namespace Löwen.Presentation.API.Services;

public class FileService(IWebHostEnvironment _env, IHttpContextAccessor _httpContextAccessor,IOptionsMonitor<StaticFilesSettings> _settings) : IFileService
{

    private static readonly Dictionary<string, List<byte[]>> _fileSignatures = new()
    {
        { ".jpg", new List<byte[]> { new byte[] { 0xFF, 0xD8 } } },
        { ".png", new List<byte[]> { new byte[] { 0x89, 0x50, 0x4E, 0x47 } } },
        { ".jpeg", new List<byte[]> { new byte[] { 0xFF, 0xD8 } } },
        { ".gif", new List<byte[]> { new byte[] { 0x47, 0x49, 0x46, 0x38 } } },
        { ".webp", new List<byte[]> { new byte[] { 0x52, 0x49, 0x46, 0x46 } } } // RIFF
    };

    private async Task<Result<string>> SaveFileAsync(IFormFile file, string folder, short maxSizeKb)
    {
        if (file == null || file.Length == 0)
            return Result.Failure<string>(new Error("Upload", "File is empty", ErrorType.NotFound));

        try
        {
            var fileSizeInKb = file.Length / 1024;
            if (fileSizeInKb > maxSizeKb)
                     return Result.Failure<string>(new Error("Upload", $"File too large. Max allowed size {maxSizeKb} KB", ErrorType.Validation));

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_settings.CurrentValue.AllowedImageExtensions.Contains(extension))
                     return Result.Failure<string>(new Error("Upload", $"Invalid file type. Allowed: {string.Join(", ", _settings.CurrentValue.AllowedImageExtensions)}", ErrorType.Validation));

            var permittedMimeTypes = new[] {"image/png","image/jpg", "image/jpeg",  "image/gif", "image/webp" };
            if (!permittedMimeTypes.Contains(file.ContentType.ToLower()))
                     return Result.Failure<string>(new Error("Upload", "Invalid content type", ErrorType.Validation));

            if (!IsValidFileSignature(file, extension))
                return Result.Failure<string>(new Error("Upload", "File signature mismatch", ErrorType.Validation));

            var fileName = Guid.NewGuid() + extension;

            var uploadPath = Path.Combine(_env.WebRootPath, folder);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var filePath = Path.Combine(uploadPath, fileName);

            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }


            return Result.Success(Path.Combine(folder, fileName).Replace("\\", "/"));

        }
        catch (Exception ex)
        {
            return Result.Failure<string>(new Error("Upload image Error", ex.Message, ErrorType.InternalServer));
        }
    }

    private bool IsValidFileSignature(IFormFile file, string extension)
    {
        if (!_fileSignatures.ContainsKey(extension))
            return false;

        var signatures = _fileSignatures[extension];
        var maxLength = signatures.Max(s => s.Length);

        using var reader = new BinaryReader(file.OpenReadStream());
        var headerBytes = reader.ReadBytes(maxLength);

        return signatures.Any(signature =>
            headerBytes.Take(signature.Length).SequenceEqual(signature));
    }

    public async Task<Result<UploadResponse>> UploadProfileImageAsync(IFormFile file)
    {
        var saveResult = await SaveFileAsync(file, _settings.CurrentValue.ProfileImages_FileName, _settings.CurrentValue.MaxProfileImageSize);
        if (saveResult.IsFailure)
            return Result.Failure<UploadResponse>(saveResult.Errors);

        return Result.Success(new UploadResponse { CurrentRootPath = _env.WebRootPath, ImagePathWithoutRootPath = saveResult.Value });
    }

    public async Task<Result<List<string>>?> UploadPostImagesAsync(List<IFormFile> files)
    {
        var urls = new List<string>();

        foreach (var file in files)
        {
            var url = await SaveFileAsync(file, _settings.CurrentValue.ProductImages_FileName, _settings.CurrentValue.MaxProductImageSize);
            if (url != null)
                urls.Add(url.Value);
        }

        return urls;
    }

}
