namespace MicroApi.Core;

public class ValidationParametersProcessor<T, TParameters> : IParametersProcessor<T, TParameters>
{
    private readonly IValidator<TParameters> _parametersValidator;
    private readonly IParametersValidationResultHandler<T, TParameters> _parametersValidationResultHandler;

    public ValidationParametersProcessor(
        IValidator<TParameters> parametersValidator,
        IParametersValidationResultHandler<T, TParameters> parametersValidationResultHandler
        )
    {
        _parametersValidator = parametersValidator;
        _parametersValidationResultHandler = parametersValidationResultHandler;
    }

    public async Task<Result<T>> ProcessAsync(TParameters key)
    {
        var validationResult = _parametersValidator.Validate(key);

        return await _parametersValidationResultHandler.HandleAsync(key, validationResult);
    }
}
