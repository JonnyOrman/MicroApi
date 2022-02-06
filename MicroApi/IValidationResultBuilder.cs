namespace MicroApi;

public interface IValidationResultBuilder
{
    void Add(ValidationRuleResult validationRuleResult);
    ValidationResult Build();
}
