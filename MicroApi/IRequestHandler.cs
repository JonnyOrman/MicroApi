using Microsoft.AspNetCore.Http;

namespace MicroApi;

public interface IRequestHandler<TParameters>
{
    Task<IResult> HandleAsync(TParameters parameters);
}