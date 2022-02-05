namespace MicroApi.Core;

public interface IValidParametersHandler<T, TParameters>
{
    Task<Result<T>> HandleAsync(TParameters parameters);
}
