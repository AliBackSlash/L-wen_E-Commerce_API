using Löwen.Domain.ErrorHandleClasses;
namespace Löwen.Domain.ConfigurationClasses.StaticFilesHelpersClasses;


public static class PicturesChecker
{
    public static Result RemoveOldPictureIfExists(string? path)
    {
     
        if(!File.Exists(path) || string.IsNullOrEmpty(path))
            return Result.Success();

		try
		{

            File.Delete(path);

            return File.Exists(path) ? Result.Failure(new Error("Image not deleted", "", ErrorType.Delete)):  Result.Success();
        }
		catch (Exception ex)
		{
            return Result.Failure(new Error("Image not deleted", ex.Message, ErrorType.Delete));
		}
    }
}
