namespace MicroApi.Core;

public interface IInvalidResultHandler<T>
{
    Result<T> Handle(InvalidResult invalidResult);
}
