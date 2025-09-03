namespace Löwen.Domain.ErrorHandleClasses;

public class Result
{
    public Result(bool isSuccess, IEnumerable<Error> errors)
    {
        ArgumentNullException.ThrowIfNull(errors);
        if (errors.Any(e => e is null))
            throw new ArgumentException("Errors collection cannot contain null values", nameof(errors));

        if (isSuccess && errors.Count() != 0 ||
            !isSuccess && errors.Count() == 0)
        {
            throw new ArgumentException("Invalid error", nameof(errors));
        }

        IsSuccess = isSuccess;
        Errors = errors;
    }
   
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    public IEnumerable<Error> Errors { get; }

    public static Result Success() => new(true, []);

    public static Result<TValue> Success<TValue>(TValue value) =>
        new(value, true, []);

    public static Result Failure(IEnumerable<Error> errors) => new(false, errors);

    public static Result<TValue> Failure<TValue>(IEnumerable<Error> errors) =>
        new(default, false, errors);

    public static Result Failure(Error error) =>
        new(false, [error]);

    public static Result<TValue> Failure<TValue>(Error error) =>
        new(default, false, [error]);
}
