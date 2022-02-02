namespace MicroApi.Core;

public interface IValidationRule<T>
{
    ValidationRuleResult Validate(T value);
}
