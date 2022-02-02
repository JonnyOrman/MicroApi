namespace MicroApi.Core;

public interface IResultTypeHandlerResolver<T>
{
    IResultTypeHandler<T> Resolve(Result<T> result);
}
