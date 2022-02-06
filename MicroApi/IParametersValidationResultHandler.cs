namespace MicroApi;

public interface IParametersValidationResultHandler<T, TParameters>
{
    Task<Result<T>> HandleAsync(TParameters parameters, ValidationResult validationResult);
}
