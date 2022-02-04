namespace MicroApi.Core;

public interface IValidationResultBuilder
{
    void Add(ValidationRuleResult validationRuleResult);
    ValidationResult Build();
}
