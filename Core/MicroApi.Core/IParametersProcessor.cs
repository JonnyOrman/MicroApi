namespace MicroApi.Core;

public interface IParametersProcessor<T, TKey>
{
    Task<Result<T>> ProcessAsync(TKey key);
}
