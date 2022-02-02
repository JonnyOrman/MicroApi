using MicroApi.Core;

namespace MicroApi.Read;

public class NotFoundResult<T, TKey> : Result<T>
{
    public NotFoundResult(
        TKey key
        )
        :
        base(
            false,
            $"{typeof(T).Name} with key {key} not found"
            )
    {
        Key = key;
    }

    public TKey Key { get; }
}
