namespace MicroApi;

public class InvalidParametersResult<T, TParameters> : Result<T>
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
