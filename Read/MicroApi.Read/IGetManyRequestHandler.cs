using Microsoft.AspNetCore.Http;

namespace MicroApi.Read;

public interface IGetManyRequestHandler<TQuery>
{
    Task<IResult> HandleAsync(TQuery query);
}