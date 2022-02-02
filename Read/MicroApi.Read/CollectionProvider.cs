using MicroApi.Core;

namespace MicroApi.Read;

public class CollectionProvider<T, TQuery> : ICollectionProvider<T, TQuery>
{
    private readonly ICollectionReader<T, TQuery> _collectionReader;

    public CollectionProvider(
        ICollectionReader<T, TQuery> collectionReader
        )
    {
        _collectionReader = collectionReader;
    }

    public async Task<Result<IEnumerable<T>>> GetAsync(TQuery query)
    {
        var result = await _collectionReader.ReadManyAsync(query);

        return new SuccessResult<IEnumerable<T>>(result);
    }
}
