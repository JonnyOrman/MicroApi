using Microsoft.AspNetCore.Http;

namespace MicroApi;

public class InvalidParametersResultTypedHandler<T, TParameters> : ITypedResultTypeHandler<InvalidParametersResult<T, TParameters>>
{
    public IResult Handle(InvalidParametersResult<T, TParameters> invalidParametersResult)
    {
        return Results.BadRequest(invalidParametersResult);
    }
}
