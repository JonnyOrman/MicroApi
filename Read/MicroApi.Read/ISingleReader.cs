namespace MicroApi.Read;

public interface ISingleReader<T, TKey>
{
    Task<T> ReadAsync(TKey key);
}
