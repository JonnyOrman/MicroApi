namespace MicroApi;

public class Result<T>
{
    public Result(
        bool isSuccessful,
        string message
        )
    {
        IsSuccessful = isSuccessful;
        Message = message;
    }

    public bool IsSuccessful { get; }

    public string Message { get; }
}
