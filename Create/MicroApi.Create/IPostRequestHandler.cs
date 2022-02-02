using Microsoft.AspNetCore.Http;

namespace MicroApi.Create;

public interface IPostRequestHandler<TParameters>
{
    Task<IResult> HandleAsync(TParameters parameters);
}
