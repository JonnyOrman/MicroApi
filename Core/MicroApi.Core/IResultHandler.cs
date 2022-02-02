using Microsoft.AspNetCore.Http;

namespace MicroApi.Core;

public interface IResultHandler<T>
{
    IResult Handle(Result<T> result);
}
