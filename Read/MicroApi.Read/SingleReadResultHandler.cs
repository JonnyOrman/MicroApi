using MicroApi.Core;

namespace MicroApi.Read;

public class SingleReadResultHandler<T, TKey> : IOperationResultHandler<T, TKey>
{
    public Result<T> Handle(T result, TKey key)
    {
        if (result != null)
        {
            return new SuccessResult<T>(
                result
            );
        }

        return new NotFoundResult<T, TKey>(
            key
        );
    }
}
