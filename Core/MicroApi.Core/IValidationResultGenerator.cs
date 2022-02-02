namespace MicroApi.Core;

public interface IValidationResultGenerator<T>
{
    ValidationResult Generate(IEnumerable<ValidationRuleResult> validationRuleResults);
}
