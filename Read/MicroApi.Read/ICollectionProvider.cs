using MicroApi.Core;

namespace MicroApi.Read;

public interface ICollectionProvider<T, TQuery>
{
    Task<Result<IEnumerable<T>>> GetAsync(TQuery query);
}
