namespace MicroApi.Core;

public interface IParametersValidationResultHandler<T, TKey>
{
    Task<Result<T>> HandleAsync(TKey key, ValidationResult validationResult);
}
