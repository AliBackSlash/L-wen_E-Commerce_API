using Löwen.Domain.ErrorHandleClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Löwen.Presentation.API.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToActionResult<T>(this Result<T> result, int successStatusCode = StatusCodes.Status200OK)
    {
        EnsureSuccessCode(successStatusCode);

        if (result.IsSuccess)
        {
            if (successStatusCode == StatusCodes.Status204NoContent)
                return new NoContentResult();

            var value = result.Value;

            return successStatusCode switch
            {
                StatusCodes.Status200OK => new OkObjectResult(value),
                StatusCodes.Status201Created => new ObjectResult(value) { StatusCode = StatusCodes.Status201Created },
                StatusCodes.Status202Accepted => new ObjectResult(value) { StatusCode = StatusCodes.Status202Accepted },
                _ => new ObjectResult(value) { StatusCode = successStatusCode }
            };
        }

        return MapErrors(result.Errors);
    }

    public static IActionResult ToActionResult(this Result result, int successStatusCode = StatusCodes.Status200OK)
    {
        EnsureSuccessCode(successStatusCode);

        if (result.IsSuccess)
        {
            return successStatusCode switch
            {
                StatusCodes.Status200OK => new OkResult(),
                StatusCodes.Status201Created => new StatusCodeResult(StatusCodes.Status201Created),
                StatusCodes.Status202Accepted => new StatusCodeResult(StatusCodes.Status202Accepted),
                StatusCodes.Status204NoContent => new NoContentResult(),
                _ => new StatusCodeResult(successStatusCode)
            };
        }

        return MapErrors(result.Errors);
    }

    private static IActionResult MapErrors(IEnumerable<Error> errors)
    {
        if (!errors.Any())
            return new StatusCodeResult(StatusCodes.Status500InternalServerError); // default to server error

        var primaryError = errors.First();

        return primaryError.Type switch
        {
            ErrorType.NotFound => new NotFoundObjectResult(errors),
            ErrorType.Validation => new BadRequestObjectResult(errors),
            ErrorType.Conflict => new ConflictObjectResult(errors),
            ErrorType.ConfirmEmailError => new ConflictObjectResult(errors),
            ErrorType.BadRequest => new BadRequestObjectResult(errors),
            ErrorType.InternalServer => new ObjectResult(errors) { StatusCode = StatusCodes.Status500InternalServerError },
            ErrorType.Unauthorized => new UnauthorizedObjectResult(errors),
            _ => new ObjectResult(errors) { StatusCode = StatusCodes.Status500InternalServerError }
        };
    }

    private static void EnsureSuccessCode(int successStatusCode)
    {
        if (successStatusCode is < 200 or >= 300)
            throw new ArgumentOutOfRangeException(nameof(successStatusCode), "Success status code must be within the 2xx range.");
    }
}
