namespace MicroApi.Core;

public interface IValidator<T>
{
    ValidationResult Validate(T value);
}
