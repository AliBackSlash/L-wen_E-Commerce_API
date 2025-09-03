namespace Löwen.Domain.ErrorHandleClasses;

public class Error
{
    public static readonly Error NullValue = new(
       "General.Null",
       "Null value was provided",
       ErrorType.Failure);

    public Error(string code, string description, ErrorType type)
    {
        Code = code;
        Description = description;
        Type = type;
    }

    public string Code { get; }

    public string Description { get; }

    public ErrorType Type { get; }
    public bool IsInternalError => Type == ErrorType.InternalServer;

    public static Error InternalServer(string code, string description) =>
            new(code, description, ErrorType.InternalServer);

    public static Error NotFound(string code, string description) =>
        new(code, description, ErrorType.NotFound);

    public static Error Conflict(string code, string description) =>
        new(code, description, ErrorType.Conflict);

    public static Error BadRequest(string code, string description) =>
    new(code, description, ErrorType.BadRequest);
    public static Error Unauthorized(string code, string description) =>
        new(code, description, ErrorType.Unauthorized);
}
