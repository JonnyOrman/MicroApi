namespace MicroApi;

public class ResultTypeValidator<T, TRequiredType> : IValidator<Result<T>>
{
    public ValidationResult Validate(Result<T> result)
    {
        var isCorrectType = typeof(TRequiredType) == result.GetType();

        return new ValidationResult(isCorrectType);
    }
}
