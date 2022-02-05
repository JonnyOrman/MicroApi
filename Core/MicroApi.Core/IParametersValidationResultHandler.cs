namespace MicroApi.Core;

public interface IParametersValidationResultHandler<T, TParameters>
{
    Task<Result<T>> HandleAsync(TParameters parameters, ValidationResult validationResult);
}
