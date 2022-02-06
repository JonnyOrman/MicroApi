namespace MicroApi;

public interface IValidator<T>
{
    ValidationResult Validate(T value);
}
