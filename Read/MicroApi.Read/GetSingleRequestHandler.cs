using MicroApi.Core;
using Microsoft.AspNetCore.Http;

namespace MicroApi.Read;

public class GetSingleRequestHandler<T, TKey> : IGetSingleRequestHandler<TKey>
{
    private readonly IParametersProcessor<T, TKey> _singleProvider;
    private readonly IResultHandler<T> _providerResultHandler;

    public GetSingleRequestHandler(
        IParametersProcessor<T, TKey> singleProvider,
        IResultHandler<T> providerResultHandler
    )
    {
        _singleProvider = singleProvider;
        _providerResultHandler = providerResultHandler;
    }

    public async Task<IResult> HandleAsync(TKey key)
    {
        var readResult = await _singleProvider.ProcessAsync(key);

        return _providerResultHandler.Handle(readResult);
    }
}