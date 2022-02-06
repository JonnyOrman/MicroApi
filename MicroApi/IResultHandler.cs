using Microsoft.AspNetCore.Http;

namespace MicroApi;

public interface IResultHandler<T>
{
    IResult Handle(Result<T> result);
}
