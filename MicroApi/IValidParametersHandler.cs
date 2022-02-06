namespace MicroApi;

public interface IValidParametersHandler<T, TParameters>
{
    Task<Result<T>> HandleAsync(TParameters parameters);
}
