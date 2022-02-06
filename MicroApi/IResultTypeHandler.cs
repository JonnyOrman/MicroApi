using Microsoft.AspNetCore.Http;

namespace MicroApi;

public interface IResultTypeHandler<T>
{
    IResult Handle(Result<T> result);
}
