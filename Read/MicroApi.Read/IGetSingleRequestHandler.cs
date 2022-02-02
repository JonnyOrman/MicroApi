using Microsoft.AspNetCore.Http;

namespace MicroApi.Read;

public interface IGetSingleRequestHandler<TKey>
{
    Task<IResult> HandleAsync(TKey key);
}