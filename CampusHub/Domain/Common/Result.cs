namespace CampusHub.Domain.Common;

public sealed class Result<T>
{
    private readonly T? _value;

    private readonly Error? _error;

    private Result(T? value, Error? error)
    {
        _value = value;
        _error = error;
    }
    
    public bool IsSuccess => _error is null;
    
    public bool IsFailure => _error is not null;
    
    public T Value => IsSuccess? _value! : throw new InvalidOperationException("Cannot access the value of a failed result.");
    
    public Error Error => IsFailure? _error! : throw new InvalidOperationException("Cannot access the error of a successful result.");
    
    public static Result<T> Success(T value) => new(value, null);
    
    public static Result<T> Failure(Error error) => new(default, error);
    
    // Implicit conversion from T to Result<T>
    public static implicit operator Result<T>(T value) => Success(value);
}