using Microsoft.AspNetCore.Http;

namespace MicroApi.Read;

public class SuccessResultTypedHandler<T> : ITypedResultTypeHandler<SuccessResult<T>>
{
    public IResult Handle(SuccessResult<T> successResult)
    {
        return Results.Ok(successResult);
    }
}
