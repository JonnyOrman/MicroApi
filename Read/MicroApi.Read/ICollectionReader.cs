namespace MicroApi.Read;

public interface ICollectionReader<T, TQuery>
{
    Task<IEnumerable<T>> ReadManyAsync(TQuery query);
}
