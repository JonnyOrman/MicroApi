using Microsoft.AspNetCore.Http;

namespace MicroApi.Create;

public class CreatedResultTypedHandler<T, TKey> : ITypedResultTypeHandler<SuccessResult<T>>
    where T : Entity<TKey>
{
    public IResult Handle(SuccessResult<T> successResult)
    {
        return Results.Created($"/{successResult.Entity.Key}", successResult);
    }
}
