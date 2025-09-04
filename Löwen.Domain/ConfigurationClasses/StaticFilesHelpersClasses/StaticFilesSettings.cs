namespace Löwen.Domain.ConfigurationClasses.StaticFilesHelpersClasses;

public class StaticFilesSettings
{
    public required string ProfileImages_FileName { get; set; }
    public required string ProductImages_FileName { get; set; }
    public required short MaxProfileImageSize { get; set; }
    public required short MaxProductImageSize { get; set; }
    public required List<string> AllowedImageExtensions { get; set; }

}
