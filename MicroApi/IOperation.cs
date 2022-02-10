namespace MicroApi;

public interface IOperation<T, TParameters>
{
    Task<T> ExecuteAsync(TParameters parameters);
}