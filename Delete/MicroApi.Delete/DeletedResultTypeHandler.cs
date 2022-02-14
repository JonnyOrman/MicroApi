using Microsoft.AspNetCore.Http;

namespace MicroApi.Delete;

public class DeletedResultTypedHandler<T, TKey> : ITypedResultTypeHandler<SuccessResult<T>>
    where T : Entity<TKey>
{
    public IResult Handle(SuccessResult<T> successResult)
    {
        return Results.Ok(successResult);
    }
}