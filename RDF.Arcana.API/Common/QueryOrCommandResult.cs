namespace RDF.Arcana.API.Common;

public class QueryOrCommandResult<T>
{
    public int Status { get; set; }
    public bool Success { get; set; }
    public T Data { get; set; }
    public List<string> Messages { get; set; } = new();
}

public class Result<T>
{
    private Result(bool isSuccess, Error error, T data = default)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid Error", nameof(error));
        }

        IsSuccess = true;
        Error = error;
        Data = data;
    }

    public T Data { get; }
    public bool IsSuccess { get; set; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; set; }
    public static Result<T> Success() => new(true, Common.Error.None);
    public static Result<T> Failure(Error error) => new(false, error);
}

public sealed record Error(string Code, string Description)
{
    public static readonly Error None = new(string.Empty, string.Empty);
}