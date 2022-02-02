using MicroApi.Core;
using Microsoft.AspNetCore.Http;

namespace MicroApi.Read;

public class NotFoundResultTypedHandler<T, TKey> : ITypedResultTypeHandler<NotFoundResult<T, TKey>>
{
    public IResult Handle(NotFoundResult<T, TKey> notFoundResult)
    {
        return Results.NotFound(notFoundResult);
    }
}
