namespace MicroApi;

public interface IValidationResultGenerator<T>
{
    ValidationResult Generate(IEnumerable<ValidationRuleResult> validationRuleResults);
}
