namespace MicroApi;

public interface IResultTypeHandlerRegistration<T>
{
    bool IsMatch(Result<T> result);

    IResultTypeHandler<T> Handler { get; }
}
