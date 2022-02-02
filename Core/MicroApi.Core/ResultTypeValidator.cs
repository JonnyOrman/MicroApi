namespace MicroApi.Core;

public class ResultTypeValidator<T, TRequiredType> : IValidator<Result<T>>
{
    public ValidationResult Validate(Result<T> value)
    {
        var isCorrectType = typeof(TRequiredType) == value.GetType();

        return new ValidationResult(isCorrectType);
    }
}
