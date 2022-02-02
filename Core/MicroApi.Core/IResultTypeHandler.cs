using Microsoft.AspNetCore.Http;

namespace MicroApi.Core;

public interface IResultTypeHandler<T>
{
    IResult Handle(Result<T> result);
}
