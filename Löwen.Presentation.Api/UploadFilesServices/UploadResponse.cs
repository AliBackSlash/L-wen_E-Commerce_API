namespace Löwen.Presentation.API.UploadFilesServices;

public class UploadResponse
{
    public required string CurrentRootPath {  get; set; }
    public required string ImagePathWithoutRootPath { get; set; }
}
