using Microsoft.AspNetCore.Http;

namespace MicroApi;

public class ResultTypeHandler<T, TResult> : IResultTypeHandler<T>
    where TResult : class
{
    private readonly ITypedResultTypeHandler<TResult> _typedResultTypeHandler;

    public ResultTypeHandler(
        ITypedResultTypeHandler<TResult> typedResultTypeHandler
    )
    {
        _typedResultTypeHandler = typedResultTypeHandler;
    }

    public IResult Handle(Result<T> result)
    {
        var typedResult = result as TResult;

        return _typedResultTypeHandler.Handle(typedResult);
    }
}
