namespace MicroApi.Core;

public class ValidationParametersProcessor<T, TKey> : IParametersProcessor<T, TKey>
{
    private readonly IValidator<TKey> _parametersValidator;
    private readonly IParametersValidationResultHandler<T, TKey> _parametersValidationResultHandler;

    public ValidationParametersProcessor(
        IValidator<TKey> parametersValidator,
        IParametersValidationResultHandler<T, TKey> parametersValidationResultHandler
        )
    {
        _parametersValidator = parametersValidator;
        _parametersValidationResultHandler = parametersValidationResultHandler;
    }

    public async Task<Result<T>> ProcessAsync(TKey key)
    {
        var validationResult = _parametersValidator.Validate(key);

        return await _parametersValidationResultHandler.HandleAsync(key, validationResult);
    }
}
