namespace MicroApi;

public interface IInvalidResultHandler<T>
{
    Result<T> Handle(InvalidResult invalidResult);
}
