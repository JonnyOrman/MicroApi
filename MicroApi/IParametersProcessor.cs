namespace MicroApi;

public interface IParametersProcessor<T, TKey>
{
    Task<Result<T>> ProcessAsync(TKey key);
}
