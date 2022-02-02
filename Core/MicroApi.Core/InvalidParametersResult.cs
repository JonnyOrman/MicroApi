namespace MicroApi.Core;

public class InvalidParametersResult<T> : Result<T>
{
    public InvalidParametersResult(
        IEnumerable<InvalidParameter> invalidParameters
        )
        :
        base(
            false,
            "Invalid parameters provided"
            )
    {
        InvalidParameters = invalidParameters;
    }

    public IEnumerable<InvalidParameter> InvalidParameters { get; }
}
