namespace Löwen.Domain.ErrorHandleClasses;

public enum ErrorType
{
    Failure = 0,
    Validation,
    Problem ,
    ConfirmEmailError,
    Delete,
    Create,
    InternalServer = 500,
    BadRequest = 400,
    NotFound = 404,
    Conflict = 409,
    Unauthorized = 401,
}
