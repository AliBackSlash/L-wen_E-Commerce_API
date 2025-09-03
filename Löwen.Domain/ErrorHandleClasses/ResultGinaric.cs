using System.Diagnostics.CodeAnalysis;

namespace Löwen.Domain.ErrorHandleClasses;

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    public Result(TValue? value, bool isSuccess, IEnumerable<Error> errors)
        : base(isSuccess, errors)
    {
        _value = value;
    }

    [NotNull]
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result can't be accessed.");

    public static implicit operator Result<TValue>(TValue? value) =>
        value is not null ? Success(value) : Failure<TValue>([Error.NullValue]);

    public static Result<TValue> ValidationFailure(IEnumerable<Error> error) =>
        new(default, false, error);
}
