namespace MicroApi.Core;

public class ParametersValidationResultHandler<T, TParameters> : IParametersValidationResultHandler<T, TParameters>
{
    private readonly IInvalidResultHandler<T> _invalidResultHandler;
    private readonly IValidParametersHandler<T, TParameters> _validParametersHandler;

    public ParametersValidationResultHandler(
        IInvalidResultHandler<T> invalidResultHandler,
        IValidParametersHandler<T, TParameters> validParametersHandler
        )
    {
        _invalidResultHandler = invalidResultHandler;
        _validParametersHandler = validParametersHandler;
    }

    public async Task<Result<T>> HandleAsync(TParameters parameters, ValidationResult validationResult)
    {
        if (!validationResult.IsSuccessful)
        {
            return _invalidResultHandler.Handle(validationResult as InvalidResult);
        }

        return await _validParametersHandler.HandleAsync(parameters);
    }
}
