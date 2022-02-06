namespace MicroApi;

public interface IValidationRule<T>
{
    ValidationRuleResult Validate(T value);
}
