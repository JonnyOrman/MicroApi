namespace MicroApi.Core;

public class ResultTypeHandlerRegistration<T> : IResultTypeHandlerRegistration<T>
{
    private readonly IValidator<Result<T>> _validator;

    public ResultTypeHandlerRegistration(
        IResultTypeHandler<T> handler,
        IValidator<Result<T>> validator
    )
    {
        Handler = handler;
        _validator = validator;
    }

    public IResultTypeHandler<T> Handler { get; }

    public bool IsMatch(Result<T> result)
    {
        var validationResult = _validator.Validate(result);

        return validationResult.IsSuccessful;
    }
}
