namespace MicroApi.Read;

public class CollectionProvider<T, TQuery> : ICollectionProvider<T, TQuery>
{
    private readonly IOperation<IEnumerable<T>, TQuery> _collectionReader;

    public CollectionProvider(
        IOperation<IEnumerable<T>, TQuery> collectionReader
        )
    {
        _collectionReader = collectionReader;
    }

    public async Task<Result<IEnumerable<T>>> GetAsync(TQuery query)
    {
        var result = await _collectionReader.ExecuteAsync(query);

        return new SuccessResult<IEnumerable<T>>(result);
    }
}
