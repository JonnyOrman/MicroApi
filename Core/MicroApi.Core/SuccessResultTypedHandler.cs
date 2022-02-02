using Microsoft.AspNetCore.Http;

namespace MicroApi.Core;

public class SuccessResultTypedHandler<T> : ITypedResultTypeHandler<SuccessResult<T>>
{
    public IResult Handle(SuccessResult<T> successResult)
    {
        return Results.Ok(successResult);
    }
}
