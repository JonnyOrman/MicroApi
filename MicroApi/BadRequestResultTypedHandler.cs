using Microsoft.AspNetCore.Http;

namespace MicroApi;

public class BadRequestResultTypedHandler<T, TParameters> : ITypedResultTypeHandler<InvalidParametersResult<T, TParameters>>
{
    public IResult Handle(InvalidParametersResult<T, TParameters> badRequestResult)
    {
        return Results.BadRequest(badRequestResult);
    }
}
