namespace MicroApi.Core;

public interface IResultTypeHandlerRegistration<T>
{
    bool IsMatch(Result<T> result);

    IResultTypeHandler<T> Handler { get; }
}
