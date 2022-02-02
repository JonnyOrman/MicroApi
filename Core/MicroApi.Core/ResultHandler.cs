using Microsoft.AspNetCore.Http;

namespace MicroApi.Core;

public class ResultHandler<T> : IResultHandler<T>
{
    private readonly IResultTypeHandlerResolver<T> _resultTypeHandlerResolver;

    public ResultHandler(
        IResultTypeHandlerResolver<T> resultTypeHandlerResolver
    )
    {
        _resultTypeHandlerResolver = resultTypeHandlerResolver;
    }

    public IResult Handle(Result<T> createResult)
    {
        var typeHandler = _resultTypeHandlerResolver.Resolve(createResult);

        return typeHandler.Handle(createResult);
    }
}
