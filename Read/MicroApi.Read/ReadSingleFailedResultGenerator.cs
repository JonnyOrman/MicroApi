namespace MicroApi.Read;

public class ReadSingleFailedResultGenerator<T, TKey> : IOperationFailedResultGenerator<T, TKey>
{
    public Result<T> Generate(TKey key)
    {
        return new NotFoundResult<T, TKey>(key);
    }
}
