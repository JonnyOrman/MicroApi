using Microsoft.AspNetCore.Http;

namespace MicroApi.Read;

public class GetManyRequestHandler<T, TQuery> : IGetManyRequestHandler<TQuery>
{
    private readonly ICollectionProvider<T, TQuery> _collectionProvider;
    private readonly IResultHandler<IEnumerable<T>> _providerResultHandler;

    public GetManyRequestHandler(
        ICollectionProvider<T, TQuery> collectionProvider,
        IResultHandler<IEnumerable<T>> providerResultHandler
        )
    {
        _collectionProvider = collectionProvider;
        _providerResultHandler = providerResultHandler;
    }

    public async Task<IResult> HandleAsync(TQuery query)
    {
        var readResult = await _collectionProvider.GetAsync(query);

        return _providerResultHandler.Handle(readResult);
    }
}