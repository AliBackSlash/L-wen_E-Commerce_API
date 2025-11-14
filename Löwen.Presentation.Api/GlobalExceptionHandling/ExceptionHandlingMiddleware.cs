using System.Net;
namespace Löwen.Presentation.GlobalExceptionHandling;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var result = new
            {
                message = "An unexpected error occurred.",
                details = context.RequestServices
                                .GetRequiredService<IWebHostEnvironment>()
                                .IsDevelopment() ? ex.Message : null
            };

            await context.Response.WriteAsJsonAsync(Result.Failure(new Error(result.message,result.details ?? "",ErrorType.InternalServer)));
        }
    }
}
