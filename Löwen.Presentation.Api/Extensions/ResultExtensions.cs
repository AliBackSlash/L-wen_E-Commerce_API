using Löwen.Domain.ErrorHandleClasses;
using Microsoft.AspNetCore.Mvc;

namespace Löwen.Presentation.API.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToActionResult<T>(this Result<T> result)
    {
        if (result.IsSuccess)
            return new OkObjectResult(result.Value);

        return MapErrors(result.Errors);
    }

    public static IActionResult ToActionResult(this Result result)
    {
        if (result.IsSuccess)
            return new OkResult();

        return MapErrors(result.Errors);
    }

    private static IActionResult MapErrors(IEnumerable<Error> errors)
    {
        if (!errors.Any())
            return new StatusCodeResult(500); // default to server error

        var primaryError = errors.First();

        return primaryError.Type switch
        {
            ErrorType.NotFound => new NotFoundObjectResult(errors),
            ErrorType.Conflict => new ConflictObjectResult(errors),
            ErrorType.ConfirmEmailError => new ConflictObjectResult(errors),
            ErrorType.BadRequest => new BadRequestObjectResult(errors),
            ErrorType.InternalServer => new ObjectResult(errors) { StatusCode = 500 },
            ErrorType.Unauthorized => new UnauthorizedObjectResult(errors),
            _ => new ObjectResult(errors) { StatusCode = 500 }
        };
    }
}
