namespace MicroApi.Core;

public class InvalidParametersResultHandler<T> : IInvalidResultHandler<T>
{
    public Result<T> Handle(InvalidResult invalidResult)
    {
        var invalidParameters = invalidResult.InvalidPropertyValues.Select(x => new InvalidParameter(x.PropertyName, x.ErrorMessages));

        return new InvalidParametersResult<T>(
            invalidParameters
        );
    }
}
